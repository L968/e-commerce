﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infra.IoC.Services;

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

    public async Task<IEnumerable<string>> UploadImage(IFormFileCollection imageFiles)
    {
        var paths = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            var imagePath = await UploadImage(imageFile);
            paths.Add(imagePath);
        }

        return paths;
    }

    public async Task RemoveImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            throw new ArgumentException("Image path cannot be null or empty", nameof(imagePath));
        }

        var blobName = Path.GetFileName(imagePath);
        var blobClient = _blobContainerClient.GetBlobClient(blobName);
        await blobClient.DeleteAsync(snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots);
    }

    public async Task RemoveImage(IEnumerable<string> imagePaths)
    {
        foreach (var imagePath in imagePaths)
        {
            await RemoveImage(imagePath);
        }
    }

    private static string GenerateUniqueImageName(string originalFileName)
    {
        var imageExtension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid()}{imageExtension}";
    }
}
