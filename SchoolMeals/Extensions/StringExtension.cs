using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Extensions
{
    public static class StringExtension
    {
        public static string ImageUrl(this string image, SectionSite sectionSite, params ImageSize[] sizes)
        {
            return string.Join("|", sizes.Select(s => $"images/{sectionSite.ToString().ToLower()}/{s.ToString().ToLower()}/{image}"));
        }
    }
}
