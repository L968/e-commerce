﻿namespace Ecommerce.Application.CartItems.Queries;

public record GetCartItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public ProductVariant? ProductVariant { get; set; }
}