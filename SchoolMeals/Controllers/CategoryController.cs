using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using SchoolMeals.Responses;
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
    public class CategoryController : ControllerBase
    {
        private IBaseRepository<Category> _categoryRepository;
        public CategoryController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.Slug = category.Name.GenerateSlug();
            Category result = await _categoryRepository.Create(category);
            return new JsonResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            category.Slug = category.Name.GenerateSlug();
            await _categoryRepository.Update(category);
            return Ok();
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.Remove(c => c.Id == id);
            return Ok();
        }
        [HttpGet]
        public async Task<JsonResult> Get(string slug)
        {
            Category category = new Category();

            if (slug != "model")
            {
                category = await _categoryRepository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
            }

            return new JsonResult(category);
        }
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Category>> result = new DataAndQuantity<IEnumerable<Category>>
            {
                Quantity = await _categoryRepository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _categoryRepository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<JsonResult> GetCategories(string lang = "ua")
        {
            return new JsonResult(await _categoryRepository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language));
        }
        public async Task<JsonResult> GetMainCategories(string lang = "ua")
        {
            return new JsonResult(await _categoryRepository.GetByFilterAsync(c => c.CategoryId == null 
                                    && c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Categories, c => c.Language));
        }
        public async Task<JsonResult> GetBreadcrumbs(string categorySlug, string subcategorySlug, string lang = "ua")
        {
            List<KeyValuePair<string, List<string>>> breadcrumbs = new List<KeyValuePair<string, List<string>>>();

            Category category = await _categoryRepository.FindByFilter(c => c.Slug.Equals(categorySlug) && c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Categories, c => c.Language);

            if(category != null)
            {
                breadcrumbs.Add(new KeyValuePair<string, List<string>>(category.Name, new List<string>() { category.Slug }));

                Category subcategory = category.Categories.SingleOrDefault(s => s.Slug.Equals(subcategorySlug));

                if (subcategory != null)
                {
                    breadcrumbs.Add(new KeyValuePair<string, List<string>>(subcategory.Name, new List<string>() { category.Slug, subcategory.Slug }));
                }
            }

            return new JsonResult(breadcrumbs);
        }        
    }
}
