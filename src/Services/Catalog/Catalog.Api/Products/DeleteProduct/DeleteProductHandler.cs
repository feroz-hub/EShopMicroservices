namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);
internal class DeleteProductCommandHandler(IDocumentSession documentSession,ILogger<DeleteProductCommandHandler> logger):ICommandHandler<DeleteProductCommand,DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Product Handler.Handle called with {@Command}",command);
        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFountException();
        documentSession.Delete<Product>(command.Id);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}