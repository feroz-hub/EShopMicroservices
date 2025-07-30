namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@query}", query);
        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        if (products == null || !products.Any())
        {
            logger.LogWarning("No products found for category: {Category}", query.Category);
            return new GetProductByCategoryResult([]);
        }

        logger.LogInformation("Found {Count} products for category: {Category}", products.Count, query.Category);
        return new GetProductByCategoryResult(products);

    }
}
