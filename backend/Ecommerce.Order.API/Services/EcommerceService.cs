using Ecommerce.Domain.DTOs;
using Ecommerce.Order.API.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Ecommerce.Order.API.Services;

public class EcommerceService(HttpClient httpClient) : IEcommerceService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<CreateOrderAddressDto?> GetAddressByIdAsync(Guid addressId)
    {
        var requestUri = new Uri($"address/{addressId}", UriKind.Relative);

        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateOrderAddressDto>(content);
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<CreateOrderProductCombinationDto?> GetProductCombinationByIdAsync(Guid productCombinationid)
    {
        var requestUri = new Uri($"productCombination/{productCombinationid}", UriKind.Relative);

        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateOrderProductCombinationDto>(content);
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task ClearCartAsync(IEnumerable<Guid> productCombinationIds)
    {
        var requestUri = new Uri("cart/clear-items", UriKind.Relative);
        var json = JsonConvert.SerializeObject(productCombinationIds);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(requestUri, httpContent);
        response.EnsureSuccessStatusCode();
    }
}
