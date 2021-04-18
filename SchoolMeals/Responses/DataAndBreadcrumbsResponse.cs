using System.Collections.Generic;

namespace SchoolMeals.Responses
{
    public class DataAndBreadcrumbsResponse<T> where T: class
    {
        public Dictionary<string, string> Breadcrumbs { get; set; }
        public T Data { get; set; }
    }
}
