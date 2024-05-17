namespace BuildingBlocks.Messaging.EventsDto;

//public record BasketItemDto(Guid ProductId, int Quantity, decimal Price);
public class BasketItemDto
{
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string Color { get; set; } = default!;
}
