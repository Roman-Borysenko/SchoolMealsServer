﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SchoolMeals.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(256, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(256, MinimumLength = 2)]
        public string Slug { get; set; }
        [Required, MinLength(16)]
        public string Description { get; set; }
        public bool IsRecommend { get; set; }
        [Required, StringLength(256, MinimumLength = 2)]
        public string Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public List<DishTag> DishTags { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        [NotMapped]
        public string[] DishUrlSections { get; set; }
        [NotMapped]
        public List<Ingredient> Ingredients { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
    }
}
