using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Enums;
using SchoolMeals.Interfaces;
using SchoolMeals.Models.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SchoolMeals.Controllers
{
    [Authorize(Roles = RolesTypes.Admin+","+ RolesTypes.Nutritionist+","+RolesTypes.Teacher + "," + RolesTypes.Director + "," + RolesTypes.HeadTeacher + "," + RolesTypes.CookingService)]
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
            string role = User.FindFirstValue(ClaimTypes.Role);

            return new JsonResult(_scheme.Schemes.Where(s => s.Roles.Contains(role)).Select(s => new Menu 
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
