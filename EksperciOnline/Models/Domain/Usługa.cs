namespace EksperciOnline.Models.Domain
{
    public class Usługa
    {
        public Guid Id { get; set; }
        public string Tytuł { get; set; }
        public string Lokalizacja { get; set; }
        public int NrTelefonu { get; set; }
        public int Cena { get; set; }
        public string Opis { get; set; }
        public string KrótkiOpis { get; set; }
        public bool Widoczność { get; set; }
        public string UrlZdjęcia { get; set; }
        public string UrlBaneru { get; set; }

        public Kategoria Kategoria { get; set; }
    }
}
