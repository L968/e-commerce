using Ecommerce.Order.API.Models.PayPal;

public class Item
{
    public string name { get; set; }
    public UnitAmount unit_amount { get; set; }
    public string quantity { get; set; }
    public string description { get; set; }
}
