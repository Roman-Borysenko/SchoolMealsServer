using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMeals.Enums;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using SchoolMeals.Requests;
using SchoolMeals.Responses;
using SchoolMeals.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private AdminService _adminService;
        private IBaseRepository<Order> _repository;
        public OrderController(AdminService adminService, IBaseRepository<Order> repository)
        {
            _adminService = adminService;
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(UserOrder order)
        {
            string userId = await _adminService.GetUserId(HttpContext);
            if(!string.IsNullOrEmpty(userId))
            {
                List<Order> orders = order.DishesIds.Select(d => new Order
                {
                    AuthorId = userId,
                    DishId = d.Key,
                    Quantity = d.Value,
                    OrderStatusId = 5,
                    CreateAt = DateTime.Now
                }).ToList();

                await _repository.CreateRange(orders);

                return Ok();
            }

            return BadRequest();
        }

        #region Admin
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<IActionResult> Update(Order order)
        {
            await _repository.Update(order);
            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Remove(c => c.Id == id);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<JsonResult> Get(string slug)
        {
            Order order = new Order();

            if (slug != "model")
            {
                if(int.TryParse(slug, out int result))
                {
                    order = await _repository.FindByFilter(c => c.Id.Equals(result), o => o.Author, o => o.Dishes, o => o.OrderStatus);
                    _adminService.OrderMapper(order);
                }
            }

            return new JsonResult(order);
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            IEnumerable<Order> orders = await _repository.GetByFilterAsync(o => true, o => o.OrderStatus.Order, OrderType.Asc, skip, take, o => o.Author, o => o.Dishes, o => o.OrderStatus);
            _adminService.OrderMapper(orders.ToArray());

            DataAndQuantity<IEnumerable<Order>> result = new DataAndQuantity<IEnumerable<Order>>
            {
                Quantity = await _repository.Count(o => true),
                Data = orders
            };

            return new JsonResult(result);
        }
        [Authorize(Roles = RolesTypes.Admin)]
        public async Task<int> OrderCount()
        {
            return await _repository.Count(o => true);
        }
        [Authorize(Roles = RolesTypes.Admin)]
        public async Task<int> TodayOrderCount()
        {
            return await _repository.Count(o => o.CreateAt > DateTime.Today);
        }
        [Authorize(Roles = RolesTypes.Admin)]
        public async Task<JsonResult> StatisticsForCurrentYear(CurrentYearStatisticRequest data)
        {
            List<CurrentYearStatisticResponse> response = new List<CurrentYearStatisticResponse>();

            if (data.Classes.Count == 0 && data.Stydents.Count == 0)
            {
                IEnumerable<Order> orders = await _repository.GetByFilterAsync(o => o.CreateAt.Year == DateTime.Today.Year);
                CurrentYearStatisticResponse statistic = GetStatisticsRecord(orders, "Всі замовлення");
                response.Add(statistic);
            } else if(data.Classes.Count != 0 && data.Stydents.Count == 0)
            {
                data.Classes = data.Classes.OrderBy(o => o).ToList();
                List<int> years = _adminService.GetYeas(data.Classes).OrderBy(o => o).ToList();

                for(int i = 0; i < years.Count; i++)
                {
                    IEnumerable<Order> orders = await _repository.GetByFilterAsync(o => o.CreateAt.Year == DateTime.Today.Year && o.Author.ReceiptDate.Year == years[i], o => o.Author);
                    CurrentYearStatisticResponse statistic = GetStatisticsRecord(orders, $"Замовлення {data.Classes[i]} класу");
                    response.Add(statistic);
                }
            } else if(data.Stydents.Count != 0)
            {
                for (int i = 0; i < data.Stydents.Count; i++)
                {
                    IEnumerable<Order> orders = await _repository.GetByFilterAsync(o => o.CreateAt.Year == DateTime.Today.Year && o.AuthorId.Equals(data.Stydents[i]), o => o.Author);

                    User user = await _adminService.GetUser(data.Stydents[i]);

                    CurrentYearStatisticResponse statistic = GetStatisticsRecord(orders, $"Замовлення {user.Surname} {user.Firstname} {user.Middlename}");
                    response.Add(statistic);
                }
            }

            return new JsonResult(response);
        }
        private CurrentYearStatisticResponse GetStatisticsRecord(IEnumerable<Order> orders, string label)
        {
            IEnumerable<IGrouping<int, Order>> groupingOrders = orders.GroupBy(o => o.CreateAt.Month);

            int maxMonth = groupingOrders.Max(o => o.Key);

            List<int?> dataChart = new List<int?>();

            for (int i = 1; i <= 12; i++)
            {
                IGrouping<int, Order> groupOrder = groupingOrders.FirstOrDefault(o => o.Key == i);

                if (groupOrder != null)
                    dataChart.Add(groupOrder.ToList().Count);
                else
                    dataChart.Add(null);
            }

            CurrentYearStatisticResponse statistic = new CurrentYearStatisticResponse
            {
                Label = label,
                Data = dataChart
            };

            return statistic;
        }
        #endregion
    }
}
