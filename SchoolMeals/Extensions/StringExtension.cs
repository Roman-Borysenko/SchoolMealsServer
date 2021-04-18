using SchoolMeals.Enums;
using System.Linq;

namespace SchoolMeals.Extensions
{
    public static class StringExtension
    {
        public static string ImageUrl(this string image, SectionSite sectionSite, params ImageSize[] sizes)
        {
            if (sizes.Length > 0)
            {
                image = string.Join("|", sizes.Select(s => $"images/{sectionSite.ToString().ToLower()}/{s.ToString().ToLower()}/{image}"));
            }
            else
            {
                image = $"images/{sectionSite.ToString().ToLower()}/{image}";
            }

            return image;
        }
    }
}
