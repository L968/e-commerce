﻿using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadImage(IFormFile file);
}
