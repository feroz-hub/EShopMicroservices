var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(); // Registers Carter endpoints
builder.Services.AddMediatR(config =>
{
    // Registers MediatR handlers from the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Adds a validation behavior for MediatR requests
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); // Adds a logging behavior for MediatR requests
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly); // Registers FluentValidation validators from the current assembly

builder.Services.AddMarten(options =>
{
    // Configures Marten with the connection string from app settings
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>(); // Registers a custom exception handler for handling exceptions globally

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(appError => { });

app.Run();