using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SchoolMeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMeals.Attributes
{
    public class CorrectUrlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            DataBaseContext dataBaseContext = context.HttpContext.RequestServices.GetService(typeof(DataBaseContext)) as DataBaseContext;

            int quantity = CreateCondition(dataBaseContext.Categories.Include(c => c.Language).Include(c => c.Dishes), context.ActionArguments).Count();

            if(quantity != 1)
            {
                context.Result = new NotFoundResult();
            }
        }

        private IQueryable<Category> CreateCondition (IQueryable<Category> expression, IDictionary<string, object> actionArguments)
        {

            List<Expression<Func<Category, bool>>> query = new List<Expression<Func<Category, bool>>>();

            if (actionArguments.ContainsKey("lang"))
            {
                query.Add(c => c.Language.NameAbbreviation.Equals(actionArguments["lang"].ToString().ToUpper()));

                if (actionArguments.ContainsKey("categorySlug"))
                {
                    query.Add(c => c.Slug.Equals(actionArguments["categorySlug"].ToString().ToLower()));

                    if (actionArguments.ContainsKey("subcategorySlug"))
                    {
                        if (actionArguments.ContainsKey("dishSlug"))
                        {
                            query.Add(c => c.Categories.Any(s => s.Slug.Equals(actionArguments["subcategorySlug"]) && s.Dishes.Any(d => d.Slug.Equals(actionArguments["dishSlug"]))));
                        }
                        else
                        {
                            query.Add(c => c.Categories.Any(s => s.Slug.Equals(actionArguments["subcategorySlug"])));
                        }
                    }
                }
            }

            return query.Aggregate(expression, (current, includeProperty) => current.Where(includeProperty));
        }
    }
}
