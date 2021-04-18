using SchoolMeals.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(16, MinimumLength = 2), Display(Name = "Назва"), DataType(DataType.Text)]
        public string Name { get; set; }
        [Required, StringLength(16, MinimumLength = 2)]
        public string Slug { get; set; }
        [Display(Name = "Батківська категорія"), DataType(CustomDataType.List)]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category ParentCategory { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public List<Category> Categories { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
