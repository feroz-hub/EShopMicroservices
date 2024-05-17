using BuildingBlocks.Messaging.EventsDto;

namespace Basket.Api.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) 
    : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator 
    : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class CheckoutBasketHandler(IBasketRepository basketRepository,IPublishEndpoint publishEndpoint):ICommandHandler<CheckoutBasketCommand,CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null)
            return new CheckoutBasketResult(false);

        var basketItems = basket.Items;
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        //eventMessage.Items = basketItems.Select(item => new BasketItemDto(item.ProductId,item.Quantity,item.Price)).ToList();
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}