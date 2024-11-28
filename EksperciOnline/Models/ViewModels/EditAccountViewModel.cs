using System.ComponentModel.DataAnnotations;

namespace EksperciOnline.Models.ViewModels
{
    public class EditAccountViewModel
    {
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Wprowadź poprawny adres e-mail.")]
        public string Email { get; set; }
    }
}
