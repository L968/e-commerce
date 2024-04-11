namespace Ecommerce.Order.API.Models.PayPal;

public class GetOrderResponse
{
    public string id { get; set; }
    public string intent { get; set; }
    public string status { get; set; }
    public PaymentSource payment_source { get; set; }
    public List<PurchaseUnit> purchase_units { get; set; }
    public Payer payer { get; set; }
    public DateTime create_time { get; set; }
    public List<Link> links { get; set; }
}

public class Address
{
    public string country_code { get; set; }
    public string address_line_1 { get; set; }
    public string admin_area_2 { get; set; }
    public string admin_area_1 { get; set; }
    public string postal_code { get; set; }
}

public class Name
{
    public string given_name { get; set; }
    public string surname { get; set; }
    public string full_name { get; set; }
}

public class Payee
{
    public string email_address { get; set; }
    public string merchant_id { get; set; }
}

public class Payer
{
    public Name name { get; set; }
    public string email_address { get; set; }
    public string payer_id { get; set; }
    public Address address { get; set; }
}

public class Shipping
{
    public Name name { get; set; }
    public Address address { get; set; }
}
