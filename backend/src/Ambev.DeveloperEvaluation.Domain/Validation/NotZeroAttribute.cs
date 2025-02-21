using System.ComponentModel.DataAnnotations;

public class NotZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && intValue <= 0)
        {
            return new ValidationResult("User  Id must be greater than zero.");
        }
        return ValidationResult.Success;
    }
}