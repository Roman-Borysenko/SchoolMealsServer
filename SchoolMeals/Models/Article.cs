using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolMeals.Models
{
    [Table("Blog")]
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Slug { get; set; }
        [Required]
        [StringLength(100000, MinimumLength = 4)]
        public string Text { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 6)]
        public string Image { get; set; }
        [JsonIgnore]
        [Required]
        public string AuthorId { get; set; }
        [JsonIgnore]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        [Required]
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
