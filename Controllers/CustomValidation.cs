namespace MVC_PROJECT.Controllers;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Access the database context
        var _context = (MyDbContext)validationContext.GetService(typeof(MyDbContext));
        var email = value as string;

        // Check if the email is already used
        if (_context.Students.Any(u => u.Email == email))
        {
            return new ValidationResult("This email address is already in use.");
        }

        return ValidationResult.Success;
    }
}
