using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishRepository _repository;
        public DishController(IDishRepository repository)
        {
            _repository = repository;
        }
        public async Task<JsonResult> GetByCategory(string categorySlug, string lang = "ua")
        {
            // TODO: add skip and take params
            return new JsonResult(await _repository.GetDishesAsync(d => d.Category.Slug.Equals(categorySlug) && d.Language.NameAbbreviation.Equals(lang.ToUpper()), 
                d => d.CreateAt, OrderType.Desc));
        }
        public async Task<JsonResult> GetNewDishes(int take, string lang = "ua")
        {
            return new JsonResult(await _repository.GetDishesAsync(d => d.Language.NameAbbreviation.Equals(lang.ToUpper()), 
                d => d.CreateAt, OrderType.Desc, 0, take));
        }
        public async Task<JsonResult> GetRecommendedDishes(int take, string lang = "ua")
        {
            return new JsonResult(await _repository.GetDishesAsync(d => d.Language.NameAbbreviation.Equals(lang.ToUpper()) && d.IsRecommend == true, 
                d => d.CreateAt, OrderType.Desc, 0, take));
        }
    }
}
