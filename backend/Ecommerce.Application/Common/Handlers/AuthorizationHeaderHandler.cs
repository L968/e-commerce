using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Ecommerce.Application.Common.Handlers;

public class AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(token))
        {
            token = token.Replace("Bearer ", "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
