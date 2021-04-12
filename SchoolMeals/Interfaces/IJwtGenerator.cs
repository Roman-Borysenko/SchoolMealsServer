using SchoolMeals.Models;

namespace SchoolMeals.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
