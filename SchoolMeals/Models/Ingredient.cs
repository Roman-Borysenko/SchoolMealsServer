using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Slug { get; set; }
        /// <summary>
        /// Calories on 100g
        /// </summary>
        [Required, Range(1, 1000)]
        public int Calories { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        //public List<Dish> Dishes { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
