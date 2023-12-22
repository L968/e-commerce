using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadImage(IFormFile file);
    Task<List<string>> UploadImage(IFormFileCollection file);
    Task RemoveImage(string imagePath);
    Task RemoveImage(List<string> imagePaths);
}
