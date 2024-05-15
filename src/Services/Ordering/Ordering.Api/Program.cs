var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container.
builder.Services
       .AddApplicationServices()
       .AddInfrastructureServices(builder.Configuration)
       .AddApiServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();
if (app.Environment.IsDevelopment()) await app.InitialiseDatabaseAsync();
app.Run();