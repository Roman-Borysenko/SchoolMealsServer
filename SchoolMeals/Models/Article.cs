using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace SchoolMeals.Models
{
    [Table("Blog")]
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [DisplayDataGrid(Name = "Назва")]
        [DisplayForm(Name = "Назва")]
        [DataType(DataType.Text)]
        [StringLength(120, MinimumLength = 2)]
        public string Title { get; set; }
        [DisplayDataGrid(Name = "Слаг")]
        [StringLength(120, MinimumLength = 2)]
        public string Slug { get; set; }
        [Required]
        [DisplayForm(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        [StringLength(100000, MinimumLength = 4)]
        public string Text { get; set; }
        [Required]
        [DisplayForm(Name = "Зображення")]
        [DataType(CustomDataType.Image)]
        [StringLength(120, MinimumLength = 6)]
        public string Image { get; set; }
        [JsonIgnore]
        public string AuthorId { get; set; }
        [JsonIgnore]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Мова", RelatedData = "api/language")]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime UpdateAt { get; set; }
        [NotMapped]
        public string AuthorName { get; set; }
    }
}
