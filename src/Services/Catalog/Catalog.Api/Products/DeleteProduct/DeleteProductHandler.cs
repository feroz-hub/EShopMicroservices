namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public class DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public record DeleteProductResult(bool IsSuccess);
internal class DeleteProductCommandHandler(IDocumentSession documentSession):ICommandHandler<DeleteProductCommand,DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFountException(command.Id);
        documentSession.Delete<Product>(command.Id);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}