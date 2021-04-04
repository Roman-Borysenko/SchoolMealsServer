using System.Collections.Generic;

namespace SchoolMeals.Requests
{
    public class DishFilterRequest
    {
        public string CategorySlug { get; set; }
        public string SubcategorySlug { get; set; }
        public string Lang { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<int> IngredientsIds { get; set; }
    }
}
