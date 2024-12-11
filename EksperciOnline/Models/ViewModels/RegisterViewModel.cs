using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EksperciOnline.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [UsernameValidation] // Użycie niestandardowej walidacji
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordComplexity]
        public string Password { get; set; }
    }

    public class UsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;

            if (string.IsNullOrWhiteSpace(username))
            {
                return new ValidationResult("Nazwa użytkownika nie może być pusta.");
            }

            // Sprawdzenie czy użytkownik zawiera polskie znaki lub spacje
            if (Regex.IsMatch(username, @"[ąćęłńóśżźĄĆĘŁŃÓŚŻŹ\s]"))
            {
                return new ValidationResult("Nazwa użytkownika nie może zawierać polskich znaków ani spacji.");
            }

            return ValidationResult.Success;
        }
    }

    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("Hasło jest wymagane.");
            }

            // Sprawdź wymagania dla hasła
            if (password.Length < 6)
                return new ValidationResult("Hasło musi mieć co najmniej 6 znaków.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Hasło musi zawierać co najmniej jedną wielką literę.");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult("Hasło musi zawierać co najmniej jedną małą literę.");

            if (!Regex.IsMatch(password, @"\d"))
                return new ValidationResult("Hasło musi zawierać co najmniej jedną cyfrę.");

            if (!Regex.IsMatch(password, @"[\W_]"))
                return new ValidationResult("Hasło musi zawierać co najmniej jeden znak specjalny.");

            return ValidationResult.Success;
        }
    }
}
