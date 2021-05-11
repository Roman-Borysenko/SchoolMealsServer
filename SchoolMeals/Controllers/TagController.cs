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
    public class TagController : ControllerBase
    {
        private IBaseRepository<Tag> _repository;

        public TagController(IBaseRepository<Tag> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            tag.Slug = tag.Name.GenerateSlug();
            Tag result = await _repository.Create(tag);
            return new JsonResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            tag.Slug = tag.Name.GenerateSlug();
            await _repository.Update(tag);
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
            Tag ingredient = new Tag();

            if (slug != "model")
            {
                ingredient = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
            }

            return new JsonResult(ingredient);
        }
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Tag>> result = new DataAndQuantity<IEnumerable<Tag>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<JsonResult> GetAll(string lang = "ua")
        {
            return new JsonResult(await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language));
        }
    }
}
