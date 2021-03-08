using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class DishTag
    {
        [Key]
        public int Id { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
