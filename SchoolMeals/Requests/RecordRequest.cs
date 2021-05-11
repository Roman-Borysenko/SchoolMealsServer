using SchoolMeals.Enums;
using System.Collections.Generic;

namespace SchoolMeals.Requests
{
    public class RecordRequest<T> where T: class
    {
        public T Data { get; set; }
        public Dictionary<ImageSize, string> Images { get; set; }
    }
}
