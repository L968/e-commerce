using Azure.Storage.Blobs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageService()
    {
        _blobServiceClient = new BlobServiceClient(Config.AzureStorageConnectionString);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(Config.AzureStorageContainer);
    }

    public async Task<string> UploadImage(IFormFile imageFile)
    {
        var imageName = GenerateUniqueImageName(imageFile.FileName);

        var blobClient = _blobContainerClient.GetBlobClient(imageName);

        using (var stream = imageFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
    }

    public async Task<List<string>> UploadImage(IFormFileCollection imageFiles)
    {
        var paths = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            var imagePath = await UploadImage(imageFile);
            paths.Add(imagePath);
        }

        return paths;
    }

    private string GenerateUniqueImageName(string originalFileName)
    {
        var imageExtension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid()}{imageExtension}";
    }
}
