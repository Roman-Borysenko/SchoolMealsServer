using Microsoft.AspNetCore.Authorization;
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
