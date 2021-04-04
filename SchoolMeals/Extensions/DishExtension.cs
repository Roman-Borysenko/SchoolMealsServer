using SchoolMeals.Models;

namespace SchoolMeals.Extensions
{
    public static class DishExtension
    {
        public static string[] DishUrl(this Dish dish)
        {
            string[] urlSections = new string[3];

            if(dish != null)
            {
                urlSections[2] = dish.Slug;

                if(dish.Category != null)
                {
                    if(dish.Category.ParentCategory != null)
                    {
                        urlSections[0] = dish.Category.ParentCategory.Slug;
                        urlSections[1] = dish.Category.Slug;
                    } else
                    {
                        urlSections[0] = dish.Category.Slug;
                    }
                }
            }

            return urlSections;
        }
    }
}
