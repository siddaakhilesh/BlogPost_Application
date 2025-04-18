namespace MVC_Application.Models.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);

    }
}
