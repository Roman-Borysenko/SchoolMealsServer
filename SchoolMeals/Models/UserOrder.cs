using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolMeals.Models
{
    public class UserOrder
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public Dictionary<int, int> DishesIds { get; set; }
    }
}
