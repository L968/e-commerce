using Ecommerce.Application.Interfaces;
using System.Net;

namespace Ecommerce.Infra.IoC.Services;

public class AuthorizationService(HttpClient httpClient) : IAuthorizationService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Guid?> GetDefaultAddressIdAsync()
    {
        var requestUri = new Uri("user/defaultAddressId", UriKind.Relative);

        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            Guid defaultAddressId = Guid.Parse(content.Replace("\"", ""));

            return defaultAddressId;
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateDefaultAddressIdAsync(Guid? addressId)
    {
        string requestUriString = addressId.HasValue
            ? "user/defaultAddressId/" + addressId
            : "user/defaultAddressId/";

        var requestUri = new Uri(requestUriString, UriKind.Relative);

        var response = await _httpClient.PatchAsync(requestUri, null);
        response.EnsureSuccessStatusCode();
    }
}
