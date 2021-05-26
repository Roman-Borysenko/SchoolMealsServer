using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolMeals.Services
{
    public class AdminService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private UserManager<User> _userManager;
        public AdminService(IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        private string SaveImages(Dictionary<ImageSize, string> images, SectionSite section)
        {
            string fileName = Guid.NewGuid().ToString().ToLower();
            string folder = Path.Combine(_hostEnvironment.WebRootPath, "images", section.ToString().ToLower());

            try 
            {
                foreach (KeyValuePair<ImageSize, string> image in images)
                {
                    Match match = Regex.Match(image.Value, "data:image/([a-z]{3,4});base64,");

                    if(match.Success)
                    {
                        if(!Regex.IsMatch(fileName, match.Groups[1].ToString()))
                        {
                            fileName = fileName + "." + match.Groups[1];
                        }

                        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(image.Value.Replace(match.Value, ""))))
                        {
                            Image img = Image.FromStream(ms);
                            img.Save(Path.Combine(folder, image.Key.ToString(), fileName));
                        }
                    }
                }
            } catch(Exception e)
            {
                return null;
            }

            return fileName;
        }
        private string GetImageNameFromUrl(string imageName)
        {
            string[] images = imageName.Split('/');
            return images[images.Length - 1];
        }

        public string GetImageName(Dictionary<ImageSize, string> images, string imageName, SectionSite section)
        {
            string img = null;

            if (images != null && images.Count != 0)
                img = SaveImages(images, section);
            else
                img = GetImageNameFromUrl(imageName);

            return img;
        }
        public async Task<string> GetUserId(HttpContext context)
        {
            string email = context.User.FindFirstValue(ClaimTypes.Email);
            User user = await _userManager.FindByEmailAsync(email);

            return user.Id;
        }
        public async Task<User> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public List<int> GetUserDiseases(HttpContext context)
        {
            string email = context.User.FindFirstValue(ClaimTypes.Email);
            User user = _userManager.Users.MultiInclude(u => u.DiseaseUsers).FirstOrDefault(u => u.NormalizedEmail.Equals(email.ToUpper()));

            return user.DiseaseUsers.Select(u => u.DiseaseId).ToList();
        }
        public Expression<Func<User, User>> SelectUser()
        {
            return user => new User
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Surname = user.Surname,
                Middlename = user.Middlename,
                UserName = user.UserName,
                Birthday = user.Birthday,
                ReceiptDate = user.ReceiptDate,
                PasswordIsChanged = user.PasswordIsChanged,
                Orders = user.Orders,
                DiseaseUsers = user.DiseaseUsers,
                DiseaseIds = user.DiseaseIds,
                Slug = user.Email,
                RoleId = user.RoleId
            };
        }
        public User UserMapper(User from, User to = null)
        {
            if (to == null)
                to = new User();

            to.Id = from.Id;
            to.Email = from.Email;
            to.Firstname = from.Firstname;
            to.Surname = from.Surname;
            to.Middlename = from.Middlename;
            to.UserName = from.UserName;
            to.Birthday = from.Birthday;
            to.ReceiptDate = from.ReceiptDate;
            to.PasswordIsChanged = from.PasswordIsChanged;
            to.Orders = from.Orders;
            to.DiseaseUsers = from.DiseaseUsers;
            to.DiseaseIds = from.DiseaseIds;
            to.Slug = from.Email;
            to.RoleId = from.RoleId;

            return to;
        }
        public void OrderMapper(params Order[] orders)
        {
            foreach (Order order in orders)
            {
                order.Slug = order.Id.ToString();
                order.Firstname = order.Author.Firstname;
                order.Surname = order.Author.Surname;
                order.DishName = order.Dishes.Name;
                order.OrderStatusName = order.OrderStatus.Name;

                order.Author = null;
                order.Dishes = null;
                order.OrderStatus = null;
            }
        }
        public List<int> GetYeas(List<int> classes)
        {
            int needYear = 0;

            if (DateTime.Today.Month >= 9 && DateTime.Today.Month <= 12)
                needYear = -1;

            List<int> receiptYears = classes.Select(c => DateTime.Today.AddYears(-(c + needYear)).Year).ToList();

            return receiptYears;
        }
    }
}
