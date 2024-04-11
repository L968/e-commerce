namespace Ecommerce.Order.API.Models.PayPal;

public class PurchaseUnit
{
    public string reference_id { get; set; }
    public Amount amount { get; set; }
    public Payee payee { get; set; }
    public List<Item> items { get; set; }
    public Shipping shipping { get; set; }
}
