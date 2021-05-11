using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.Interfaces;
using SchoolMeals.Models.Admin;
using System.Collections.Generic;
using System.Linq;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IModelScheme _scheme;
        public AdminController(IModelScheme schema)
        {
            _scheme = schema;
        }
        public JsonResult GetAdminMenu()
        {
            return new JsonResult(_scheme.Schemes.Select(s => new Menu 
            { 
                Name = s.Name, 
                Slug = s.Slug
            }));
        }
        public IActionResult GetScheme(string modelName, string operation) 
        {
            Scheme scheme = _scheme.GetScheme(modelName, operation);

            if (scheme == null)
                return NotFound();

            return new JsonResult(scheme);
        }
    }
}
