namespace Catalog.Api.Products.GetProducts;

public record GetProductsRequest(int? PageNumber=1,int? PageSize=10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var products = await sender.Send(query);
                var response = products.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product")
            .WithDescription("Get product");
    }
}