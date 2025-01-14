using CartsService.Consumers;
using CartsService.Data;
using CartsService.Services;
using MassTransit;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ProductCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("cart", false));

    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        configuration.ReceiveEndpoint("cart-product-created", e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 5));

            e.ConfigureConsumer<ProductCreatedConsumer>(context);
        });

        configuration.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<CartService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    await Policy.Handle<TimeoutException>()
        .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
        .ExecuteAndCaptureAsync(async () => await DbInitializer.InitDb(app));
});


app.Run();
