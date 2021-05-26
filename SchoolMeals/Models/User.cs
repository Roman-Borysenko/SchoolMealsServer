using Microsoft.AspNetCore.Identity;
using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMeals.Models
{
    public class User : IdentityUser
    {
        [DisplayForm(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
        [DisplayDataGrid(Name = "Ім'я")]
        [DisplayForm(Name = "Ім'я")]
        [DataType(DataType.Text)]
        [StringLength(32, MinimumLength = 2)]
        public string Firstname { get; set; }
        [DisplayDataGrid(Name = "Прізвище")]
        [DisplayForm(Name = "Прізвище")]
        [DataType(DataType.Text)]
        [StringLength(32, MinimumLength = 2)]
        public string Surname { get; set; }
        [DisplayDataGrid(Name = "По Батькові")]
        [DisplayForm(Name = "По Батькові")]
        [DataType(DataType.Text)]
        [StringLength(32, MinimumLength = 2)]
        public string Middlename { get; set; }
        [Required]
        [DisplayForm(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        [DisplayForm(Name = "Дата початку навчання")]
        [DataType(DataType.Date)]
        public DateTime ReceiptDate { get; set; }
        public bool? PasswordIsChanged { get; set; }
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Роль", RelatedData = "api/user/getroles")]
        public string RoleId { get; set; }
        public List<Order> Orders { get; set; }
        public List<DiseaseUser> DiseaseUsers { get; set; }

        [NotMapped]
        [DisplayDataGrid(Name = "Email")]
        public string Slug { get; set; }
        [NotMapped]
        [DataType(CustomDataType.Multiselect)]
        [DisplayForm(Name = "Захворювання", RelatedData = "api/disease/getall")]
        public List<int> DiseaseIds { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
    }
}
