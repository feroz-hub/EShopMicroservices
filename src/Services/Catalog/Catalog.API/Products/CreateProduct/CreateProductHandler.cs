

namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Command to create a new product.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Price">The price of the product.</param>
/// <param name="ImageFile">The image file name or URL of the product.</param>
/// <param name="Category">The list of categories the product belongs to.</param>
public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category
) : ICommand<CreateProductResult>;

/// <summary>
/// Result returned after creating a product.
/// </summary>
/// <param name="Id">The unique identifier of the created product.</param>
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("At least one product category is required.");
    }
}

/// <summary>
/// Handles the <see cref="CreateProductCommand"/> to create a new product.
/// </summary>
internal class CreateProductCommandHandler (IDocumentSession session,ILogger<CreateProductCommandHandler> logger): ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the creation of a new product.
    /// </summary>
    /// <param name="command">The command containing product details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The result containing the new product's ID.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

        // Create a new Product instance using the details from the command
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Category = command.Category
        };
        // Store the new product in the session
        session.Store(product);
        // Persist changes to the database
        await session.SaveChangesAsync(cancellationToken);
        // Return the result containing the new product's ID
        return new CreateProductResult(product.Id);
    }
}