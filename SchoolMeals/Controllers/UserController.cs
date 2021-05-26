using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.Interfaces;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using SchoolMeals.Responses;
using SchoolMeals.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBaseRepository<DiseaseUser> _diseas;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private AdminService _adminService;
        private ILogger<UserController> _logger;
        private IJwtGenerator _jwt;
        public UserController(
            IBaseRepository<DiseaseUser> diseas,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            AdminService adminService,
            ILogger<UserController> logger,
            IJwtGenerator jwt)
        {
            _diseas = diseas;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _adminService = adminService;
            _logger = logger;
            _jwt = jwt;
        }
        [HttpPost]
        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await _userManager.CreateAsync(user, "Schoolmeals1.");
            } catch (Exception e)
            {
                _logger.LogError(e.ShowError());
                return false;
            }

            return true;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            User user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);

            IdentityRole role = await _roleManager.FindByIdAsync(user.RoleId);
            user.RoleName = role?.Name;

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (result.Succeeded)
            {
                return new JsonResult(new AuthUser
                {
                    Name = user.UserName,
                    Email = user.Email,
                    PasswordIsChanged = user.PasswordIsChanged,
                    IsAdmin = !string.IsNullOrEmpty(user.RoleName),
                    Token = user.PasswordIsChanged != null && user.PasswordIsChanged == true ? _jwt.CreateToken(user) : null
                });

            }

            return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(Login login)
        {
            PasswordValidator<User> passValidator = new PasswordValidator<User>();
            var passValidResult = await passValidator.ValidateAsync(_userManager, null, login.NewPassword);

            if (!passValidResult.Succeeded)
                return BadRequest(passValidResult.Errors);

            User user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);

            IdentityRole role = await _roleManager.FindByIdAsync(user.RoleId);
            user.RoleName = role?.Name;

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (result.Succeeded)
            {
                await _userManager.ChangePasswordAsync(user, login.Password, login.NewPassword);
                user.PasswordIsChanged = true;
                await _userManager.UpdateAsync(user);

                return new JsonResult(new AuthUser
                {
                    Name = user.UserName,
                    Email = user.Email,
                    PasswordIsChanged = user.PasswordIsChanged,
                    IsAdmin = !string.IsNullOrEmpty(user.RoleName),
                    Token = user.PasswordIsChanged != null && user.PasswordIsChanged == true ? _jwt.CreateToken(user) : null
                });
            }

            return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }
        public IActionResult TokenCheck()
        {
            return Ok();
        }

        #region Admin
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin)]
        public IActionResult GetUserByClasses(List<int> classes)
        {
            List<User> users = new List<User>();

            List<int> receiptYears = _adminService.GetYeas(classes);

            if (classes != null && classes.Count > 0)
                users = _userManager.Users.Where(u => receiptYears.Contains(u.ReceiptDate.Year) && string.IsNullOrEmpty(u.RoleId)).ToList();
            else
                users = _userManager.Users.Where(u => string.IsNullOrEmpty(u.RoleId)).ToList();

            users.ForEach(u => u.UserName = $"{u.Surname} {u.Firstname} {u.Middlename} ({DateTime.Today.Year - u.ReceiptDate.Year} класс)");

            return new JsonResult(users);
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public async Task<IActionResult> Create(User user)
        {
            user.DiseaseUsers = user.DiseaseIds.Select(i => new DiseaseUser { UserId = user.Id, DiseaseId = i }).ToList();
            user.UserName = user.Firstname;
            await _userManager.CreateAsync(user, "Schoolmeals1.");
            await AddRoleToUser(user);

            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public async Task<IActionResult> Update(User user)
        {
            User toUpdate = await _userManager.FindByIdAsync(user.Id);

            if(toUpdate == null)
            {
                ModelState.AddModelError(nameof(user.Id), "Invalid id entered");
                return BadRequest(ModelState);
            }

            await _diseas.Remove(t => t.UserId == user.Id);
            user.DiseaseUsers = user.DiseaseIds.Select(i => new DiseaseUser { UserId = user.Id, DiseaseId = i }).ToList();

            _adminService.UserMapper(user, toUpdate);

            await _userManager.UpdateAsync(toUpdate);
            await AddRoleToUser(toUpdate);
            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public JsonResult Get(string slug)
        {
            User user = new User
            {
                DiseaseIds = new List<int>()
            };

            if (slug != "model")
            {
                user = _adminService.UserMapper(_userManager.Users.MultiInclude(u => u.DiseaseUsers).SingleOrDefault(u => u.Email.Equals(slug)));
                if (user.DiseaseUsers == null)
                    user.DiseaseIds = new List<int>();
                else
                    user.DiseaseIds = user.DiseaseUsers.Select(d => d.DiseaseId).ToList();
            }

            return new JsonResult(user);
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public JsonResult GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<User>> result = new DataAndQuantity<IEnumerable<User>>
            {
                Quantity = _userManager.Users.Count(),
                Data = _userManager.Users.Select(_adminService.SelectUser()).Skip(skip).Take(take).ToList()
            };

            return new JsonResult(result);
        }
        [Authorize(Roles = RolesTypes.Admin)]
        public int UserCount()
        {
            return _userManager.Users.Count();
        }
        [Authorize(Roles = RolesTypes.Admin)]
        public int UserWithDiseasesCount()
        {
            return _userManager.Users.MultiInclude(u => u.DiseaseUsers).Where(u => u.DiseaseUsers != null && u.DiseaseUsers.Count > 0).Count();
        }
        public JsonResult GetRoles()
        {
            return new JsonResult(_roleManager.Roles.ToList());
        }
        private async Task AddRoleToUser(User user)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(user.RoleId);

            foreach (string roleName in _roleManager.Roles.Select(r => r.Name).ToList())
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }

            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }
        [AllowAnonymous]
        public async Task FirstData()
        {
            List<IdentityRole> roles = new List<IdentityRole> 
            { 
                new IdentityRole { Name = RolesTypes.Admin },
                new IdentityRole { Name = RolesTypes.Nutritionist },
                new IdentityRole { Name = RolesTypes.Teacher },
                new IdentityRole { Name = RolesTypes.Director },
                new IdentityRole { Name = RolesTypes.HeadTeacher },
                new IdentityRole { Name = RolesTypes.CookingService },
            };

            foreach(IdentityRole role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }
        #endregion
    }
}
