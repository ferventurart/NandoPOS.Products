using Application;
using Application.Abstractions.EventBus;
using Application.Behaviors;
using Carter;
using FluentValidation;
using Infrastructure.MessageBroker;
using Marten;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(Presentation.PresentationAssembly.Instance);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Products Microservices - NandoPOS", Version = "v1" });
});

builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host("localhost", "/", h =>
        {
            h.Username(settings.UserName);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddMediatR(ApplicationAssembly.Instance);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(ApplicationAssembly.Instance, includeInternalTypes: true);

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
