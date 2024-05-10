using Discount.Grpc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Service to the Container
builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();