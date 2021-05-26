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
    public class DiseaseController : ControllerBase
    {
        private IBaseRepository<Disease> _repository;
        public DiseaseController(IBaseRepository<Disease> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.Nutritionist)]
        public async Task<IActionResult> Create(Disease disease)
        {
            disease.Slug = disease.Name.GenerateSlug();
            Disease result = await _repository.Create(disease);
            return new JsonResult(result);
        }
        [HttpPost]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.Nutritionist)]
        public async Task<IActionResult> Update(Disease disease)
        {
            disease.Slug = disease.Name.GenerateSlug();
            await _repository.Update(disease);
            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.Nutritionist)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Remove(c => c.Id == id);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.Nutritionist)]
        public async Task<JsonResult> Get(string slug)
        {
            Disease disease = new Disease();

            if (slug != "model")
            {
                disease = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
            }

            return new JsonResult(disease);
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher + "," + RolesTypes.Nutritionist)]
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Disease>> result = new DataAndQuantity<IEnumerable<Disease>>
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
