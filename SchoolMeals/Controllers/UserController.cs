using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolMeals.Extensions;
using SchoolMeals.Interfaces;
using SchoolMeals.Models;
using SchoolMeals.Responses;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ILogger<UserController> _logger;
        private IJwtGenerator _jwt;
        public UserController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<UserController> logger,
            IJwtGenerator jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwt = jwt;
        }
        [HttpPost]
        public async Task<bool> CreateUser(string email, string name)
        {
            User user = new User
            {
                UserName = name,
                Email = email
            };

            try
            {
                await _userManager.CreateAsync(user, "Schoolmeals1.");
            } catch(Exception e)
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

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (result.Succeeded)
            {
                return new JsonResult(new AuthUser
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Token = _jwt.CreateToken(user)
                });

            }

            return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }
        public IActionResult TokenCheck()
        {
            return Ok();
        }
    }
}
