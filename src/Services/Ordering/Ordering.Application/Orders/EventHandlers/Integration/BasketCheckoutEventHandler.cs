namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender,ILogger<BasketCheckoutEventHandler> logger):IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = MapToCreateOrderCommand(context.Message);
        
        await sender.Send(command);
    }
    private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        if (message.BasketItems == null)
        {
            throw new ArgumentNullException(nameof(message.BasketItems), "Basket items cannot be null");
        }
        
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();
        var orderItems = message.BasketItems.Select(item =>
            new OrderItemDto(
                orderId,
                item.ProductId,
                item.Quantity,
                item.Price
            )
        ).ToList();

        //var orderItems = message.Items.Select(item => new OrderItemDto (orderId, item.ProductId, item.Quantity, item.Price)).ToList();
        // var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto, OrderStatus.Pending, [
        //     new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
        //     new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
        // ]); 
        var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto, OrderStatus.Pending,orderItems);
        return new CreateOrderCommand(orderDto);
    }
}