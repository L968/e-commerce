using Ecommerce.Application.Common.Interfaces;
using System.Security.Claims;

namespace Ecommerce.API.Services;

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
