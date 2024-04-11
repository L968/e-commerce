namespace Ecommerce.Order.API.Models.PayPal;

public class CreateOrderRequest
{
    public string intent { get; set; }
    public List<PurchaseUnit> purchase_units { get; set; }
    public PaymentSource payment_source { get; set; }
}
