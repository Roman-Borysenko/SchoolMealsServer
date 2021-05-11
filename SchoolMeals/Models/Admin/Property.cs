using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System.Collections.Generic;

namespace SchoolMeals.Models.Admin
{
    public class Property
    {
        public string PropName { get; set; }
        public Dictionary<string, string> DisplayData { get; set; }
        public string Type { get; set; }
        public Dictionary<ImageSize, List<int>> ImagesSize { get; set; }
    }
}
