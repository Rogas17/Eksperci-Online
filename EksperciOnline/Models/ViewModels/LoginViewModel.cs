using System.ComponentModel.DataAnnotations;

namespace EksperciOnline.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Hasło musi mieć coanjmniej 6 znaków")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; } = "/";
    }
}
