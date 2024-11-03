using EksperciOnline.Models.Domain;

namespace EksperciOnline.Models.ViewModels
{
    public class EditCategoryRequest
    {
        public Guid Id { get; set; }
        public string NazwaKategorii { get; set; }
        public string UrlZdjęcia { get; set; }
    }
}
