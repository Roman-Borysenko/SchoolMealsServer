using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Ingredient
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
        /// <summary>
        /// Calories on 100g
        /// </summary>
        [DisplayDataGrid(Name = "Калорії на 100 грам")]
        [DisplayForm(Name = "Калорії на 100 грам")]
        [DataType(CustomDataType.Number)]
        [Required, Range(1, 1000)]
        public int Calories { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Мова", RelatedData = "api/language/getall")]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
