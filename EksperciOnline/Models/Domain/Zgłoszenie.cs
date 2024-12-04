namespace EksperciOnline.Models.Domain
{
    public class Zgłoszenie
    {
        public Guid Id { get; set; }
        public Guid UsługaId { get; set; }
        public string Powód { get; set; }
        public string Opis { get; set; }
        public DateTime DataZgłoszenia { get; set; }
        public bool CzyRozpatrzone { get; set; }
        public bool CzyZablokowane { get; set; }

        // Nawigacja
        public Usługa Usługa { get; set; }
    }
}
