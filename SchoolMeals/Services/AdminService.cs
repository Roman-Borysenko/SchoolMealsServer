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
    }
}
