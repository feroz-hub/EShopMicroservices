namespace Catalog.API.Products.GetProducts;

//public record GetProductsRequest();

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async(ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .WithSummary("Get all products")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("This endpoint retrieves all products from the catalog.");
    }
}
