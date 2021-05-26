namespace SchoolMeals.Responses
{
    public class AuthUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? PasswordIsChanged { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
