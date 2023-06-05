﻿using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.DTO.ProductDto;

public record CreateProductDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    public string? Description { get; init; }

    [Required]
    public string? Sku { get; init; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public decimal? Price { get; init; }

    [Required]
    public bool? Active { get; init; }

    [Required]
    public bool? Visible { get; set; }

    [Required]
    [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public float? Length { get; set; }

    [Required]
    [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public float? Width { get; set; }

    [Required]
    [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public float? Height { get; set; }

    [Required]
    [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public float? Weight { get; init; }

    [Required]
    public int? ProductCategoryId { get; init; }

    [Required]
    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFileCollection? Images { get; init; }
}