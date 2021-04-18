using SchoolMeals.Models.Admin;

namespace SchoolMeals.Interfaces
{
    public interface IModelSchema
    {
        Schema GetSchema(string modelName);
    }
}
