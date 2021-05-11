using System;

namespace SchoolMeals.DataAnnotations
{
    public class DisplayFormAttribute : Attribute
    {
        public string Name { get; set; }
        public string RelatedData { get; set; }
    }
}
