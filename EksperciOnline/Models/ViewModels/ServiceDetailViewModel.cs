using EksperciOnline.Models.Domain;

namespace EksperciOnline.Models.ViewModels
{
    public class ServiceDetailViewModel
    {
        public Guid Id { get; set; }
        public string Tytuł { get; set; }
        public string Lokalizacja { get; set; }
        public int? NrTelefonu { get; set; }
        public double CenaOd { get; set; }
        public double CenaDo { get; set; }
        public string Opis { get; set; }
        public string KrótkiOpis { get; set; }
        public bool Widoczność { get; set; }
        public string? UrlZdjęcia { get; set; }
        public string? UrlBaneru { get; set; }
        public DateTime DataPulikacji { get; set; }
        public Guid AutorId { get; set; }
        public string Autor {  get; set; }
        public Kategoria Kategoria { get; set; }

        public int TotalLikes { get; set; }

        public int TotalComments { get; set; }

        public string CommentDescription { get; set; }

        public int Grade { get; set; }

        public double AverageGrade { get; set; }

        public IEnumerable<ServiceCommentViewModel> Comments { get; set; }

    }
}
