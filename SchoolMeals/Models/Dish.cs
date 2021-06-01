using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SchoolMeals.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        [DisplayDataGrid(Name = "Назва")]
        [DisplayForm(Name = "Назва")]
        [DataType(DataType.Text)]
        [Required, StringLength(256, MinimumLength = 2)]
        public string Name { get; set; }
        [DisplayDataGrid(Name = "Слаг")]
        [StringLength(256, MinimumLength = 2)]
        public string Slug { get; set; }
        [DisplayForm(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        [Required, MinLength(16)]
        public string Description { get; set; }
        [DisplayForm(Name = "Рекомендована страва")]
        [DataType(CustomDataType.CheckBox)]
        public bool IsRecommend { get; set; }
        [DisplayForm(Name = "Зображення")]
        [DataType(CustomDataType.Image)]
        public string Image { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Категорія", RelatedData = "api/category/getcategories")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Мова", RelatedData = "api/language/getall")]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        [JsonIgnore]
        public string AuthorId { get; set; }
        [JsonIgnore]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        public List<DishTag> DishTags { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<DiseaseDish> DiseaseDishes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        /*--Properties for response--*/
        [NotMapped]
        public string[] DishUrlSections { get; set; }
        [NotMapped]
        public List<Ingredient> Ingredients { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public List<Disease> Diseases { get; set; }
        [NotMapped, Required]
        [DataType(CustomDataType.Multiselect)]
        [DisplayForm(Name = "Інградієнти", RelatedData = "api/ingredient/getall")]
        public List<int> IngredientsIds { get; set; }
        [NotMapped, Required]
        [DataType(CustomDataType.Multiselect)]
        [DisplayForm(Name = "Теги", RelatedData = "api/tag/getall")]
        public List<int> TagsIds { get; set; }
        [NotMapped, Required]
        [DataType(CustomDataType.Multiselect)]
        [DisplayForm(Name = "Рекомендовано при захворюваннях", RelatedData = "api/disease/getall")]
        public List<int> DiseaseIds { get; set; }
    }
}
