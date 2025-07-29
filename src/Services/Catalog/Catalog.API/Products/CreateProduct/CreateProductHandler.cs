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

/// <summary>
/// Handles the <see cref="CreateProductCommand"/> to create a new product.
/// </summary>
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the creation of a new product.
    /// </summary>
    /// <param name="command">The command containing product details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The result containing the new product's ID.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Category = command.Category
        };
        return new CreateProductResult(Guid.NewGuid());
    }
}