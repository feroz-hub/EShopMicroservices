
namespace Catalog.API.Products.GetProductByCategory;
//public record GetProductByCategoryRequest();

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResult>();
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .WithSummary("Get products by category")
        .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("This endpoint retrieves products by category from the catalog.");
    }
}
