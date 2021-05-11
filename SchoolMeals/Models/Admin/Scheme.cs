using System.Collections.Generic;

namespace SchoolMeals.Models.Admin
{
    public class Scheme
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public Dictionary<string, string>  Url { get; set; }
        public List<Property> Properties { get; set; }
    }
}
