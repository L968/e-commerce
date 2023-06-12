using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public int? UserId => int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("id")!.Value!);

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}