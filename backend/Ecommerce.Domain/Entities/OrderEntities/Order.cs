namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Order : BaseEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public int UserId { get; private set; }
    public decimal TotalProducts { get; private set; }
    public decimal? ShippingCost { get; private set; }
    public decimal? Discount { get; private set; }
    public decimal Total { get; private set; }
    public string ShippingPostalCode { get; private set; }
    public string ShippingStreetName { get; private set; }
    public string ShippingBuildingNumber { get; private set; }
    public string? ShippingComplement { get; private set; }
    public string? ShippingNeighborhood { get; private set; }
    public string? ShippingCity { get; private set; }
    public string? ShippingState { get; private set; }
    public string? ShippingCountry { get; private set; }

    public Order(
        int userId,
        decimal totalProducts,
        decimal? shippingCost,
        decimal? discount,
        decimal total,
        string shippingPostalCode,
        string shippingStreetName,
        string shippingBuildingNumber,
        string? shippingComplement,
        string? shippingNeighborhood,
        string? shippingCity,
        string? shippingState,
        string? shippingCountry
    ) {
        UserId = userId;
        TotalProducts = totalProducts;
        ShippingCost = shippingCost;
        Discount = discount;
        Total = total;
        ShippingPostalCode = shippingPostalCode;
        ShippingStreetName = shippingStreetName;
        ShippingBuildingNumber = shippingBuildingNumber;
        ShippingComplement = shippingComplement;
        ShippingNeighborhood = shippingNeighborhood;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingCountry = shippingCountry;
    }

    public Order(
        int id,
        Guid guid,
        int userId,
        decimal totalProducts,
        decimal? shippingCost,
        decimal? discount,
        decimal total,
        string shippingPostalCode,
        string shippingStreetName,
        string shippingBuildingNumber,
        string? shippingComplement,
        string? shippingNeighborhood,
        string? shippingCity,
        string? shippingState,
        string? shippingCountry
    ) {
        Id = id;
        Guid = guid;
        UserId = userId;
        TotalProducts = totalProducts;
        ShippingCost = shippingCost;
        Discount = discount;
        Total = total;
        ShippingPostalCode = shippingPostalCode;
        ShippingStreetName = shippingStreetName;
        ShippingBuildingNumber = shippingBuildingNumber;
        ShippingComplement = shippingComplement;
        ShippingNeighborhood = shippingNeighborhood;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingCountry = shippingCountry;
    }

    public void Update(
        int userId,
        decimal totalProducts,
        decimal? shippingCost,
        decimal? discount,
        decimal total,
        string shippingPostalCode,
        string shippingStreetName,
        string shippingBuildingNumber,
        string? shippingComplement,
        string? shippingNeighborhood,
        string? shippingCity,
        string? shippingState,
        string? shippingCountry
    ) {
        UserId = userId;
        TotalProducts = totalProducts;
        ShippingCost = shippingCost;
        Discount = discount;
        Total = total;
        ShippingPostalCode = shippingPostalCode;
        ShippingStreetName = shippingStreetName;
        ShippingBuildingNumber = shippingBuildingNumber;
        ShippingComplement = shippingComplement;
        ShippingNeighborhood = shippingNeighborhood;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingCountry = shippingCountry;
    }
}