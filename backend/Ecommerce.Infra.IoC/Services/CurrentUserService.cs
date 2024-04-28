using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.Infra.IoC.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public int UserId => GetUserId();

    private int GetUserId()
    {
        string? userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

        return !string.IsNullOrEmpty(userIdString)
            ? int.Parse(userIdString)
            : -1;
    }
}
