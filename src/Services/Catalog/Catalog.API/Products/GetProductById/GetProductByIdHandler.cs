namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Query to get a product by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the product.</param>
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

/// <summary>
/// Result containing the product retrieved by the query.
/// </summary>
/// <param name="Product">The product entity.</param>
public record GetProductByIdResult(Product Product);

/// <summary>
/// Handles the retrieval of a product by its unique identifier.
/// </summary>
/// <param name="session">The document session for database access.</param>

internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    /// <summary>
    /// Handles the GetProductByIdQuery and returns the product if found.
    /// </summary>
    /// <param name="query">The query containing the product ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result containing the product.</returns>
    /// <exception cref="ProductNotFoundException">Thrown if the product is not found.</exception>
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        return product == null ? throw new ProductNotFoundException(query.Id) : new GetProductByIdResult(product);
    }
}
