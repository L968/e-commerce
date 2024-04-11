namespace Ecommerce.Order.API.Models.PayPal;

public class Paypal
{
    public string email_address { get; set; }
    public string account_id { get; set; }
    public string account_status { get; set; }
    public Name name { get; set; }
    public Address address { get; set; }
    public ExperienceContext experience_context { get; set; }
}
