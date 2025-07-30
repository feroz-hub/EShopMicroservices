
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    decimal Price,
    string ImageUrl) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Product ID cannot be empty.");
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters long.");
        //RuleFor(command => command.Category)
        //    .NotEmpty().WithMessage("Product category cannot be empty.");
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Product description cannot be empty.")
            .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");
        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        //RuleFor(command => command.ImageUrl)
        //    .NotEmpty().WithMessage("Product image URL cannot be empty.");

    }
}

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {Id} not found", command.Id);
            throw new ProductNotFoundException();
        }
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageUrl;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product with ID {Id} updated successfully", command.Id);
        return new UpdateProductResult(true);
    }
}
