using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [DisplayDataGrid(Name = "Назва")]
        [DisplayForm(Name = "Назва")]
        [DataType(DataType.Text)]
        [Required, StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
        [DisplayDataGrid(Name = "Слаг")]
        [StringLength(16, MinimumLength = 2)]
        public string Slug { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Мова", RelatedData = "api/language/getall")]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        //public List<Dish> Dishes { get; set; }
        public List<DishTag> DishTags { get; set; }
    }
}
