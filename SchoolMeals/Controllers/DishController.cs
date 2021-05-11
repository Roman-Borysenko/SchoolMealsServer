using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Attributes;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using SchoolMeals.Requests;
using SchoolMeals.Responses;
using SchoolMeals.Services;
using SlugGenerator;
using System;
using System.Collections.Generic;
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
        private IBaseRepository<DishTag> _tag;
        private IBaseRepository<DishIngredient> _ingredient;
        private AdminService _adminService;
        public DishController(IDishRepository repository, IBaseRepository<DishTag> tag, IBaseRepository<DishIngredient> ingredient, AdminService adminService)
        {
            _repository = repository;
            _tag = tag;
            _ingredient = ingredient;
            _adminService = adminService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecordRequest<Dish> data)
        {
            Dish dish = data.Data;

            if (dish.IngredientsIds.Count == 0)
            {
                ModelState.AddModelError(nameof(dish.IngredientsIds), "You have not selected any ingredients");
                return BadRequest(ModelState);
            }

            dish.Slug = dish.Name.GenerateSlug();
            dish.Image = _adminService.GetImageName(data.Images, dish.Image, SectionSite.Dishes);
            dish.CreateAt = DateTime.Now;
            dish.UpdateAt = DateTime.Now;
            dish.AuthorId = await _adminService.GetUserId(HttpContext);

            Dish newDish = await _repository.Create(dish);

            newDish.DishIngredients = dish.IngredientsIds.Select(i => new DishIngredient { DishId = newDish.Id, IngredientId = i }).ToList();
            newDish.DishTags = dish.TagsIds.Select(i => new DishTag { DishId = newDish.Id, TagId = i }).ToList();

            await _repository.Update(newDish);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Update(RecordRequest<Dish> data)
        {
            Dish dish = data.Data;

            if (dish.IngredientsIds.Count == 0)
            {
                ModelState.AddModelError(nameof(dish.IngredientsIds), "You have not selected any ingredients");
                return BadRequest(ModelState);
            }

            await _tag.Remove(t => t.DishId == dish.Id);
            await _ingredient.Remove(t => t.DishId == dish.Id);

            dish.Slug = dish.Name.GenerateSlug();
            dish.Image = _adminService.GetImageName(data.Images, dish.Image, SectionSite.Dishes);
            dish.DishIngredients = dish.IngredientsIds.Select(i => new DishIngredient { DishId = dish.Id, IngredientId = i }).ToList();
            dish.DishTags = dish.TagsIds.Select(i => new DishTag { DishId = dish.Id, TagId = i }).ToList();
            dish.UpdateAt = DateTime.Now;

            await _repository.Update(dish);

            return Ok();
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Remove(c => c.Id == id);
            return Ok();
        }
        [HttpGet]
        public async Task<JsonResult> Get(string slug)
        {
            Dish dishes = new Dish
            {
                IngredientsIds = new List<int>(),
                TagsIds = new List<int>()
            };

            if (slug != "model")
            {
                dishes = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()), d => d.DishIngredients, d => d.DishTags);
                dishes.Image = dishes.Image.ImageUrl(SectionSite.Dishes, ImageSize.Min, ImageSize.Midd, ImageSize.Max);
                dishes.IngredientsIds = dishes.DishIngredients.Select(d => d.IngredientId).ToList();
                dishes.TagsIds = dishes.DishTags.Select(d => d.TagId).ToList();
            }

            return new JsonResult(dishes);
        }
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Dish>> result = new DataAndQuantity<IEnumerable<Dish>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
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
