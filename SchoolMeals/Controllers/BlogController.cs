using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
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
    public class BlogController : ControllerBase
    {
        private IBaseRepository<Article> _repository;
        public BlogController(IBaseRepository<Article> repository)
        {
            _repository = repository;
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
