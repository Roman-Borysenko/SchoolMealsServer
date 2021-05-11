using System.Collections.Generic;

namespace SchoolMeals.Models.Admin
{
    public class Menu
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public Dictionary<string, Dictionary<string, string>> Urls { get; set; }
    }
}
