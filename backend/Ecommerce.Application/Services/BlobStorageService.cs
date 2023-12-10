using Azure.Storage.Blobs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Services;

public class BlobStorageService : IBlobStorageService
{
    public async Task<string> UploadImage(IFormFile imageFile)
    {
        var blobServiceClient = new BlobServiceClient(Config.AzureStorageConnectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(Config.AzureStorageContainer);

        var imageExtension = Path.GetExtension(imageFile.FileName);
        var imageName = $"{Guid.NewGuid()}{imageExtension}";

        var blobClient = blobContainerClient.GetBlobClient(imageName);

        using (var stream = imageFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        var blobUri = blobClient.Uri;
        return blobUri.ToString();
    }
}
