using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Extensions
{
    public static class StringExtension
    {
        public static string ImageUrl(this string image, SectionSite sectionSite, ImageSize size)
        {
            return $"images/{sectionSite.ToString().ToLower()}/{size.ToString().ToLower()}/{image}";
        }
    }
}
