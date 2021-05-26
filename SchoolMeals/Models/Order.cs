using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using System;
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
        [DisplayDataGrid(Name = "Кількість")]
        public int Quantity { get; set; }
        [Required, Range(1, 1000000)]
        [DataType(CustomDataType.List)]
        [DisplayForm(Name = "Статус замовлення", RelatedData = "api/orderstatus/getall")]
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        [DisplayForm(Name = "Час замовлення")]
        [DataType(CustomDataType.Disable)]
        public DateTime CreateAt { get; set; }

        /*For admin*/
        [NotMapped]
        [DisplayDataGrid(Name = "Прізвище")]
        [DisplayForm(Name = "Прізвище")]
        [DataType(CustomDataType.Disable)]
        public string Surname { get; set; }
        [NotMapped]
        [DisplayDataGrid(Name = "Ім'я")]
        [DisplayForm(Name = "Ім'я")]
        [DataType(CustomDataType.Disable)]
        public string Firstname { get; set; }
        [NotMapped]
        [DisplayDataGrid(Name = "Страва")]
        [DisplayForm(Name = "Страва")]
        [DataType(CustomDataType.Disable)]
        public string DishName { get; set; }
        [NotMapped]
        [DisplayDataGrid(Name = "Статус замовлення")]
        public string OrderStatusName { get; set; }
        [NotMapped]
        [DisplayDataGrid(Name = "Id")]
        public string Slug { get; set; }
    }
}
