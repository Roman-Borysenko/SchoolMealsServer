using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBaseRepository<Article> _repository;
        private AdminService _adminService;
        public BlogController(IBaseRepository<Article> repository, AdminService adminService)
        {
            _repository = repository;
            _adminService = adminService;
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        [HttpPost]
        public async Task<IActionResult> Create(RecordRequest<Article> data)
        {
            Article article = data.Data;
            article.Slug = article.Title.GenerateSlug();
            article.Image = _adminService.GetImageName(data.Images, article.Image, SectionSite.Blog);
            article.CreateAt = DateTime.Now;
            article.UpdateAt = DateTime.Now;
            article.AuthorId = await _adminService.GetUserId(HttpContext);

            await _repository.Create(article);

            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        [HttpPost]
        public async Task<IActionResult> Update(RecordRequest<Article> data)
        {
            Article article = data.Data;
            article.Slug = article.Title.GenerateSlug();
            article.Image = _adminService.GetImageName(data.Images, article.Image, SectionSite.Blog);
            article.UpdateAt = DateTime.Now;

            await _repository.Update(article);

            return Ok();
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Remove(c => c.Id == id);
            return Ok();
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        [HttpGet]
        public async Task<JsonResult> Get(string slug)
        {
            Article articles = new Article();

            if (slug != "model")
            {
                articles = await _repository.FindByFilter(c => c.Slug.Equals(slug.ToLower()));
                articles.Image = articles.Image.ImageUrl(SectionSite.Blog, ImageSize.Min, ImageSize.Max);
            }

            return new JsonResult(articles);
        }
        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.HeadTeacher)]
        public async Task<JsonResult> GetForAdmin(int skip, int take, string lang = "ua")
        {
            DataAndQuantity<IEnumerable<Article>> result = new DataAndQuantity<IEnumerable<Article>>
            {
                Quantity = await _repository.Count(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), c => c.Language),
                Data = await _repository.GetByFilterAsync(c => c.Language.NameAbbreviation.Equals(lang.ToUpper()), skip, take, c => c.Language)
            };

            return new JsonResult(result);
        }
        public async Task<IActionResult> GetArticles(int skip, int take, string lang = "ua")
        {
            List<Article> articles = await _repository.GetByFilterAsync(a => a.Language.NameAbbreviation.Equals(lang), a => a.UpdateAt, OrderType.Desc, skip, take, a => a.Language, a => a.Author) as List<Article>;
            articles.ForEach(a => 
            { 
                a.Image = a.Image.ImageUrl(SectionSite.Blog, ImageSize.Min); 
                a.AuthorName = a.Author?.UserName;
                a.Author = null;
                a.AuthorId = string.Empty;
            });

            return new JsonResult(articles);
        }
        public async Task<IActionResult> GetArticle(string slug)
        {
            Article article = await _repository.FindByFilter(a => a.Slug.Equals(slug), a => a.Author);

            article.Image = article.Image.ImageUrl(SectionSite.Blog, ImageSize.Max);
            article.AuthorName = article.Author?.UserName;
            article.Author = null;
            article.AuthorId = string.Empty;

            return new JsonResult(article);
        }
    }
}
