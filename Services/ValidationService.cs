using System.Text.RegularExpressions;

namespace GroupProject.Services;

public class ValidationService : IValidationService
{
    public bool IsValidEmail(string email)
    {
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public bool IsValid(string str)
    {
        return str is { Length: > 6 };
    }
}