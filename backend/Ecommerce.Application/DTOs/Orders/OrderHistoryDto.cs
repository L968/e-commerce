namespace Ecommerce.Application.DTOs;

public record OrderHistoryDto
{
    public string Status { get; init; } = "";
    public string? Notes { get; init; }
    public DateTime Date { get; init; }
}
