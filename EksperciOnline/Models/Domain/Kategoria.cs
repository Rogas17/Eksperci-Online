namespace EksperciOnline.Models.Domain
{
    public class Kategoria
    {
        public Guid Id { get; set; }
        public string NazwaKategorii { get; set; }
        public string UrlZdjęcia { get; set; }
        public ICollection<Usługa> Usługi { get; set; }
    }
}
