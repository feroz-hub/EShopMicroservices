
namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductRequest();
public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {

            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            var response = new DeleteProductResponse(result.IsSuccess);
            if (!response.IsSuccess)
            {
                return Results.Problem("Failed to delete product", statusCode: StatusCodes.Status500InternalServerError);
            }
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Deletes a product by its unique identifier. Returns 200 OK if successful, 404 if not found, or 500 if an error occurs.");

    }
}
