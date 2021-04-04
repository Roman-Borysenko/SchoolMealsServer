using System;

namespace SchoolMeals.Extensions
{
    public static class ExceptionExtension
    {
        public static string ShowError(this Exception exception)
        {
            return $"{exception.Message}\n{exception.StackTrace}\n\n";
        }
    }
}
