using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMeals.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMeals.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IModelSchema _schema;
        public AdminController(IModelSchema schema)
        {
            _schema = schema;
        }
        public JsonResult GetSchema() 
        {
            return new JsonResult(_schema.GetSchema("Category"));
        }
    }
}
