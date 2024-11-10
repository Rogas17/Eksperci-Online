namespace EksperciOnline.Repositiories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
