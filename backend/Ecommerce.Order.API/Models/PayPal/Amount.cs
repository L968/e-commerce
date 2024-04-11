namespace Ecommerce.Order.API.Models.PayPal;

public class Amount
{
    public string currency_code { get; set; }
    public string value { get; set; }
    public Breakdown breakdown { get; set; }
}
