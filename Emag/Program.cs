using Emag.Data;
using Emag.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x =>
{
    //x.AddEntityFrameworkOutbox<ProductDbContext>(o =>
    //{
    //    o.QueryDelay = TimeSpan.FromSeconds(10);

    //    o.UsePostgres();
    //    o.UseBusOutbox();
    //});

    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ProductService>();

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

try
{
    DbInitializer.InitDb(app);
}
catch( Exception e)
{
    Console.WriteLine(e);
}

app.Run();
