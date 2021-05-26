using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class DiseaseDish
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int DiseaseId { get; set; }
        [ForeignKey("DiseaseId")]
        public Disease Disease { get; set; }
    }
}
