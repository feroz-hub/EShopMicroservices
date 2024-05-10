namespace Catalog.Api.Products.CreateProduct;

public  record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public  record CreateProductResponse(Guid Id);

public class CreateProductEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}",response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create product")
        .WithDescription("Create product");
    }
}
// public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
// public record CreateProductResponse(Guid Id);
//
// public class CreateProductEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapPost("/products", async (IEnumerable<CreateProductRequest> requests, ISender sender) =>
//             {
//                 var responses = new List<CreateProductResponse>();
//
//                 foreach (var request in requests)
//                 {
//                     var command = request.Adapt<CreateProductCommand>();
//                     var result = await sender.Send(command);
//                     var response = result.Adapt<CreateProductResponse>();
//                     responses.Add(response);
//                 }
//
//                 return Results.CreatedAtRoute("GetProducts", new { ids = responses.Select(r => r.Id) }, responses);
//             })
//             .WithName("CreateProducts")
//             .Produces<IEnumerable<CreateProductResponse>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithSummary("Create products")
//             .WithDescription("Create multiple products");
//     }
// }
// public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
// public record CreateProductResponse(Guid Id);
//
// public class CreateProductEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapPost("/products", async (List<CreateProductRequest> requests, ISender sender) =>
//             {
//                 var createProductCommands = requests.Select(request => request.Adapt<CreateProductCommand>()).ToList();
//                 var tasks = createProductCommands.Select(command => sender.Send(command)).ToList();
//                 await Task.WhenAll(tasks);
//
//                 var responses = tasks.Select(task => task.Result.Adapt<CreateProductResponse>()).ToList();
//                 var ids = responses.Select(response => response.Id).ToList();
//
//                 return Results.Created($"/products", ids);
//             })
//             .WithName("CreateProduct")
//             .Produces<List<Guid>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithSummary("Create products")
//             .WithDescription("Create multiple products at once");
//     }
// }

