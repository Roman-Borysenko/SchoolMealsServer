using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class IngredientController : ControllerBase
    {
        private IBaseRepository<Ingredient> _repository;
        public IngredientController(IBaseRepository<Ingredient> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            ingredient.Slug = ingredient.Name.GenerateSlug();
            Ingredient result = await _repository.Create(ingredient);
            return new JsonResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Ingredient ingredient)
        {
            ingredient.Slug = ingredient.Name.GenerateSlug();
            await _repository.Update(ingredient);
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
            Ingredient ingredient = new Ingredient();

            if (slug != "model")
            {
                ingredient = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
            }

            return new JsonResult(ingredient);
        }
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Ingredient>> result = new DataAndQuantity<IEnumerable<Ingredient>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<JsonResult> GetAll(string lang = "ua")
        {
            return new JsonResult(await _repository.GetByFilterAsync(i => i.Language.NameAbbreviation.Equals(lang.ToUpper()), i => i.Language));
        }
    }
}
