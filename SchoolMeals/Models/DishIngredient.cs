using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class DishIngredient
    {
        [Key]
        public int Id { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }
    }
}
