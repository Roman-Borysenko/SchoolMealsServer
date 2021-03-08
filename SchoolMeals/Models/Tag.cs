using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Slug { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        //public List<Dish> Dishes { get; set; }
        public List<DishTag> DishTags { get; set; }
    }
}
