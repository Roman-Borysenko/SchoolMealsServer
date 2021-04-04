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
    }
}
