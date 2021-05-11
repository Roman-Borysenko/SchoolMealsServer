namespace SchoolMeals.Responses
{
    public class DataAndQuantity<T> where T: class
    {
        public DataAndQuantity() { }
        public DataAndQuantity(int quantity, T data)
        {
            Quantity = quantity;
            Data = data;
        }
        public int Quantity { get; set; }
        public T Data { get; set; }
    }
}
