using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using SchoolMeals.Responses;
using SlugGenerator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private IBaseRepository<OrderStatus> _repository;
        public OrderStatusController(IBaseRepository<OrderStatus> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<IActionResult> Create(OrderStatus orderStatus)
        {
            orderStatus.Slug = orderStatus.Name.GenerateSlug();
            OrderStatus result = await _repository.Create(orderStatus);
            return new JsonResult(result);
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<IActionResult> Update(OrderStatus orderStatus)
        {
            orderStatus.Slug = orderStatus.Name.GenerateSlug();
            await _repository.Update(orderStatus);
            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Remove(c => c.Id == id && c.CanDelete == true);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<JsonResult> Get(string slug)
        {
            OrderStatus orderStatus = new OrderStatus();

            if (slug != "model")
            {
                orderStatus = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
            }

            return new JsonResult(orderStatus);
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<OrderStatus>> result = new DataAndQuantity<IEnumerable<OrderStatus>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()) && c.CanDelete == true, skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<JsonResult> GetAll(string lang = "ua")
        {
            return new JsonResult(await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), o => o.Order, OrderType.Asc, c => c.Language));
        }
    }
}
