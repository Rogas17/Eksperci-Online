using System.ComponentModel.DataAnnotations;

namespace EksperciOnline.Models.ViewModels
{
    public class AddCategoryRequest
    {
        [Required]
        public string NazwaKategorii { get; set; }
        [Required]
        public string UrlZdjęcia { get; set; }
    }
}
