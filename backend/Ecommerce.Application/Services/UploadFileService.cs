using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application.Services;

public class UploadFileService : IUploadFileService
{
    public List<string> UploadFiles(IFormFileCollection files)
    {
        throw new NotImplementedException();
        //string folder = "uploads";
        //var rootImagesPaths = new List<string>();
        //var apiImagesPaths = new List<string>();
        //string rootImagesFolder = Path.Combine(_environment.WebRootPath, folder);

        //try
        //{
        //    if (!Directory.Exists(rootImagesFolder))
        //    {
        //        Directory.CreateDirectory(rootImagesFolder);
        //    }

        //    foreach (var file in files)
        //    {
        //        string fileExtension = Path.GetExtension(file.FileName);
        //        string fileName = Guid.NewGuid().ToString() + fileExtension;

        //        string rootImagePath = Path.Combine(rootImagesFolder, fileName);
        //        string apiImagePath = $"{_configuration["ApiUrl"]}/{folder}/{fileName}";

        //        using (var fileStream = File.Create(rootImagePath))
        //        {
        //            file.CopyTo(fileStream);
        //            fileStream.Flush();
        //        }

        //        apiImagesPaths.Add(apiImagePath);
        //        rootImagesPaths.Add(rootImagePath);
        //    }

        //    return apiImagesPaths;
        //}
        //catch (Exception)
        //{
        //    foreach (var rootImagePath in rootImagesPaths)
        //    {
        //        File.Delete(rootImagePath);
        //    }

        //    throw;
        //}
    }
}