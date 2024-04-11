namespace Ecommerce.Order.API.Models.PayPal;

public class CreateOrderResponse
{
    public string id { get; set; }
    public string status { get; set; }
    public List<Link> links { get; set; }
}
