using Ecommerce.Order.API.Models.PayPal;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Order.API.Services;

public class PayPalService(HttpClient httpClient) : IPayPalService
{
    private string _accessToken = "";
    private DateTime _accessTokenExpiration;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<GetOrderResponse?> GetOrderAsync(string orderId)
    {
        await RefreshAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        var response = await _httpClient.GetAsync($"/v2/checkout/orders/{orderId}");
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetOrderResponse>(responseBody);
    }

    /// <summary>
    /// Creates a PayPal payment for the specified order.
    /// </summary>
    /// <returns>The approval URL for the created payment</returns>
    public async Task<CreateOrderResponse> CreateOrderAsync()
    {
        await RefreshAccessToken();

        var order = new CreateOrderRequest
        {
            intent = "CAPTURE",
            purchase_units =
            [
                new PurchaseUnit
                {
                    items =
                    [
                        new Item
                        {
                            name = "T-Shirt",
                            description = "Green XL",
                            quantity = "1",
                            unit_amount = new UnitAmount
                            {
                                currency_code = "CAD",
                                value = "100.00"
                            }
                        }
                    ],
                    amount = new Amount
                    {
                        currency_code = "CAD",
                        value = "100.00",
                        breakdown = new Breakdown
                        {
                            item_total = new ItemTotal
                            {
                                currency_code = "CAD",
                                value = "100.00"
                            }
                        }
                    }
                }
            ],
            payment_source = new PaymentSource
            {
                paypal = new Paypal
                {
                    experience_context = new ExperienceContext
                    {
                        return_url = Config.PayPalReturnUrl,
                        cancel_url = Config.PayPalCancelUrl
                    }
                }
            }
        };

        string paymentJson = JsonSerializer.Serialize(order);
        var content = new StringContent(paymentJson, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        var response = await _httpClient.PostAsync("/v2/checkout/orders", content);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CreateOrderResponse>(responseBody)!;
    }

    private async Task RefreshAccessToken()
    {
        if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now < _accessTokenExpiration)
            return;

        string username = Config.PayPalClientId;
        string password = Config.PayPalClientSecret;
        string credentials = $"{username}:{password}";
        string encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);

        var collection = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("ignoreCache", "true"),
        ]);

        var response = await _httpClient.PostAsync("/v1/oauth2/token", collection);
        response.EnsureSuccessStatusCode();

        string? responseBody = await response.Content.ReadAsStringAsync();
        TokenInfo token = JsonSerializer.Deserialize<TokenInfo>(responseBody)!;

        _accessToken = token.access_token;
        _accessTokenExpiration = DateTime.Now.AddSeconds(token.expires_in);
    }
}
