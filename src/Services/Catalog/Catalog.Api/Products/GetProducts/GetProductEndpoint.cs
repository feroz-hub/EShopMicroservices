namespace Catalog.Api.Products.GetProducts;

// public record GetProductRequest;
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var products = await sender.Send(new GetProductsQuery());
            var response = products.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product")
            .WithDescription("Get product"); }
}