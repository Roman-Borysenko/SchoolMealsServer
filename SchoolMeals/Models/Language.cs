using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolMeals.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(2)]
        public string NameAbbreviation { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Slug { get; set; }
        public bool Default { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
    }
}
