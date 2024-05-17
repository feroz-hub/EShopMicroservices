namespace BuildingBlocks.Messaging.EventsDto;

public record BasketItemDto(Guid ProductId, int Quantity, decimal Price);
