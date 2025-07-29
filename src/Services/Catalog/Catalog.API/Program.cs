var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(); // Registers Carter endpoints
builder.Services.AddMediatR(config =>
{
    // Registers MediatR handlers from the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    // Configures Marten with the connection string from app settings
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter(); // Maps Carter endpoints to the app
app.Run(); // Runs the application