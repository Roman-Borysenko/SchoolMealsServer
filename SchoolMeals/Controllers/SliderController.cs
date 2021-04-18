using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private IBaseRepository<Slide> _repository;
        public SliderController(IBaseRepository<Slide> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> GetSlidesToShow(string lang = "ua")
        {
            List<Slide> slides = (await _repository.GetByFilterAsync(s => s.IsShow == true && s.Language.NameAbbreviation.Equals(lang.ToUpper()), s => s.CreateAt, OrderType.Desc, 0, 3, s => s.Language) as List<Slide>);
            slides.ForEach(s => s.Image = s.Image.ImageUrl(SectionSite.Slider));

            return new JsonResult(slides);
        }
    }
}
