namespace EksperciOnline.Models.Domain
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Localisation { get; set; }
        public int PhoneNumber { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool Visibility { get; set; }
        public string ImageUrl { get; set; }
        public string BanerUrl { get; set; }

        public Category Category { get; set; }
    }
}
