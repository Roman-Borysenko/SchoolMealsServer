using Microsoft.AspNetCore.Mvc;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IBaseRepository<Ingredient> _repository;
        public IngredientController(IBaseRepository<Ingredient> repository)
        {
            _repository = repository;
        }
        public async Task<JsonResult> GetAll(string lang = "ua")
        {
            return new JsonResult(await _repository.GetByFilterAsync(i => i.Language.NameAbbreviation.Equals(lang.ToUpper()), i => i.Language));
        }
    }
}
