using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Interfaces;

public interface IUploadFileService
{
    public List<string> UploadFiles(IFormFileCollection files);
}