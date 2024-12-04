using EksperciOnline.Models.Domain;

public interface IZgłoszenieRepository
{
    Task<IEnumerable<Zgłoszenie>> GetAllAsync();
    Task<Zgłoszenie> GetAsync(Guid id);
    Task AddAsync(Zgłoszenie zgłoszenie);
    Task UpdateAsync(Zgłoszenie zgłoszenie);
}
