using System.Collections.Generic;

namespace SchoolMeals.Responses
{
    public class CurrentYearStatisticResponse
    {
        public string Label { get; set; }
        public List<int?> Data { get; set; }
    }
}
