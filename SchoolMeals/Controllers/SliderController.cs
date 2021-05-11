using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private IBaseRepository<Slide> _repository;
        private AdminService _adminService;
        public SliderController(IBaseRepository<Slide> repository, AdminService adminService)
        {
            _repository = repository;
            _adminService = adminService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecordRequest<Slide> data)
        {
            Slide slide = data.Data;
            slide.Slug = slide.Title.GenerateSlug();
            slide.Image = _adminService.GetImageName(data.Images, slide.Image, SectionSite.Slider);
            slide.CreateAt = DateTime.Now;
            slide.UpdateAt = DateTime.Now;
            slide.AuthorId = await _adminService.GetUserId(HttpContext);

            await _repository.Create(slide);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Update(RecordRequest<Slide> data)
        {
            Slide slide = data.Data;
            slide.Slug = slide.Title.GenerateSlug();
            slide.Image = _adminService.GetImageName(data.Images, slide.Image, SectionSite.Slider);
            slide.UpdateAt = DateTime.Now;

            await _repository.Update(slide);

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
            Slide slides = new Slide();

            if (slug != "model")
            {
                slides = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
                slides.Image = slides.Image.ImageUrl(SectionSite.Slider, ImageSize.Max);
            }

            return new JsonResult(slides);
        }
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Slide>> result = new DataAndQuantity<IEnumerable<Slide>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<IActionResult> GetSlidesToShow(string lang = "ua")
        {
            List<Slide> slides = (await _repository.GetByFilterAsync(s => s.IsShow == true && s.Language.NameAbbreviation.Equals(lang.ToUpper()), s => s.CreateAt, OrderType.Desc, 0, 3, s => s.Language) as List<Slide>);
            slides.ForEach(s => s.Image = s.Image.ImageUrl(SectionSite.Slider, ImageSize.Max));

            return new JsonResult(slides);
        }
    }
}
