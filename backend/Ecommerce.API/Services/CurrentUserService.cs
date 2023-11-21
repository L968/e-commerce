using Ecommerce.Application.Common.Interfaces;
using System.Security.Claims;

namespace Ecommerce.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public int UserId => GetUserId();

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId()
    {
        string? userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

        return !string.IsNullOrEmpty(userIdString)
            ? int.Parse(userIdString)
            : -1;
    }
}
