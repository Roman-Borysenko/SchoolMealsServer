using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    [Table("Slider")]
    public class Slide
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 4)]
        public string Description { get; set; }
        [StringLength(30, MinimumLength = 2)]
        public string ButtonName { get; set; }
        [StringLength(120, MinimumLength = 12)]
        public string ButtonLink { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 6)]
        public string Image { get; set; }
        [Required]
        public bool IsShow { get; set; }
        [Required]
        public string AuthorId { get; set; }
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
    }
}
