using Application;
using Application.Behaviors;
using Application.Products.CreateProduct;
using Carter;
using FluentValidation;
using Marten;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(Presentation.PresentationAssembly.Instance);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Products Microservices - NandoPOS", Version = "v1" });
});

builder.Services.AddMediatR(ApplicationAssembly.Instance);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(ApplicationAssembly.Instance, includeInternalTypes: true);

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>(ServiceLifetime.Transient);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.MapControllers();

app.Run();
