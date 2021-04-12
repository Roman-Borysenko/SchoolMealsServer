using Microsoft.AspNetCore.Identity;
using System;

namespace SchoolMeals.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime ReceiptDate { get; set; }
    }
}
