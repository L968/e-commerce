using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Ecommerce.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly HttpClient _httpClient;

    public AuthorizationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(3);
        _httpClient.BaseAddress = new Uri(configuration["AuthorizationBaseUrl"] ?? throw new InvalidOperationException("AuthorizationBaseUrl configuration is missing or empty"));
    }

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
