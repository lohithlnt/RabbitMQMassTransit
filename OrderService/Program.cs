using MassTransit;
using RabbitMQ.Client;
using SharedLib.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        var uri = new Uri(builder.Configuration["ServiceBus:Uri"]);
        cfg.Host(uri, host =>
        {
            host.Username(builder.Configuration["ServiceBus:Username"]);
            host.Password(builder.Configuration["ServiceBus.Password"]);
        });
        //exchange
        cfg.Message<Order>(c => c.SetEntityName("order"));

        //below code sometimes not working with Direct just comment 
        //cfg.Publish<Order>(x =>
        //{
        //    x.ExchangeType = ExchangeType.Direct;
        //});

        //cfg.Publish<Order>(x =>
        //{
        //    x.Durable = false; //default:true
        //    x.AutoDelete = true; //default : false
        //    x.ExchangeType = "fanout"; //default, allows any valid exchange type
        //});
    });
});

builder.Services.AddMassTransitHostedService(true);

// Add services to the container.

builder.Services.AddControllers();
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

app.Run();
