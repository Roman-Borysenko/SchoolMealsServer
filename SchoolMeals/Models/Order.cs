using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dishes { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
