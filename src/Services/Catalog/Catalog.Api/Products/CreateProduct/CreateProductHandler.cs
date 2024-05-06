

using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater that 0");
    }
}

public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession documentSession,ILogger<CreateProductCommandHandler> logger):ICommandHandler<CreateProductCommand,CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Product Handler.Handle called with {@Command}",command);
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken); 
        return new CreateProductResult(product.Id);
    }
}