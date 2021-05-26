using System.Collections.Generic;

namespace SchoolMeals.Requests
{
    public class CurrentYearStatisticRequest
    {
        public List<int> Classes { get; set; }
        public List<string> Stydents { get; set; }
    }
}
