using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.IRepositories;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private IBaseRepository<Language> _repository;
        public LanguageController(IBaseRepository<Language> repository)
        {
            _repository = repository;
        }
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await _repository.GetAllAsync());
        }
    }
}
