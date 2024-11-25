using System.ComponentModel.DataAnnotations;

namespace EksperciOnline.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Hasło musi mieć conajmniej 6 znaków")]
        public string Password { get; set; }
    }
}
