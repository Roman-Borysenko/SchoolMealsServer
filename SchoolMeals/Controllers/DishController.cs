using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Attributes;
using SchoolMeals.Enums;
using SchoolMeals.IRepositories;
using SchoolMeals.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishRepository _repository;
        public DishController(IDishRepository repository)
        {
            _repository = repository;
        }
        [CorrectUrl]
        public async Task<JsonResult> GetByCategory(string categorySlug, string subcategorySlug, int skip = 0, int take = 10, string lang = "ua")
        {
            return new JsonResult(await _repository.GetDishesAsync(d => d.Category.Slug.Equals(string.IsNullOrEmpty(subcategorySlug) ? categorySlug : subcategorySlug) 
                && d.Language.NameAbbreviation.Equals(lang.ToUpper()), 
                d => d.CreateAt, OrderType.Desc, skip, take));
        }
        [HttpPost]
        public async Task<JsonResult> GetByFilter(DishFilterRequest request)
        {
            // TODO: rewrite
            return new JsonResult(await _repository.GetDishesAsync(d => 
                d.Category.Slug.Equals(string.IsNullOrEmpty(request.SubcategorySlug) ? request.CategorySlug : request.SubcategorySlug)
                && d.Language.NameAbbreviation.Equals(request.Lang.ToUpper())
                && (request.IngredientsIds.Count() > 0 ? d.DishIngredients.Any(di => request.IngredientsIds.Contains(di.IngredientId)) : true),
                d => d.CreateAt, OrderType.Desc, request.Skip, request.Take));
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
        public async Task<JsonResult> GetRecommendedDishesFromCategory(string categorySlug, int take, string lang = "ua")
        {
            return new JsonResult(await _repository.GetDishesAsync(d => d.Language.NameAbbreviation.Equals(lang.ToUpper()) 
                && d.Category.Slug.Equals(categorySlug)
                && d.IsRecommend == true,
                d => d.CreateAt, OrderType.Desc, 0, take));
        }
        public async Task<JsonResult> GetDish(string categorySlug, string subcategorySlug, string dishSlug, string lang = "ua")
        {
            //TODO: write a method to check if a subcategory is correct
            return new JsonResult(await _repository.GetDishAsync(d => ((!string.IsNullOrEmpty(subcategorySlug) && !subcategorySlug.Equals("dish")) ? d.Category.ParentCategory.Slug.Equals(categorySlug) : true)
                && d.Category.Slug.Equals((!string.IsNullOrEmpty(subcategorySlug) && !subcategorySlug.Equals("dish")) ? subcategorySlug : categorySlug)
                && d.Slug.Equals(dishSlug) 
                && d.Language.NameAbbreviation.Equals(lang.ToUpper())));
        }
    }
}
