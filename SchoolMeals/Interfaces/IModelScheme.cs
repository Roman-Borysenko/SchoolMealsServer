using SchoolMeals.Models.Admin;
using System.Collections.Generic;

namespace SchoolMeals.Interfaces
{
    public interface IModelScheme
    {
        List<Menu> Schemes { get; }
        Scheme GetScheme(string modelName, string operations);
    }
}
