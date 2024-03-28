namespace GroupProject.Services;

public interface IValidationService
{
    bool IsValidEmail(string email);
    bool IsValid(string str);
}