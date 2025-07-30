using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(); // Registers Carter endpoints
builder.Services.AddMediatR(config =>
{
    // Registers MediatR handlers from the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Adds a validation behavior for MediatR requests
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly); // Registers FluentValidation validators from the current assembly

builder.Services.AddMarten(options =>
{
    // Configures Marten with the connection string from app settings
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Unhandled Exception: {contextFeature.Error}");

            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred!",
                Status = context.Response.StatusCode,
                Detail = contextFeature.Error.Message
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    });
});

app.Run();