using Dapr;
using Microsoft.EntityFrameworkCore;
using OrderMS.DBContext;
using OrderMS.Models;
using OrderMS.Repository;
using OrderMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.MapPost("/orders-tickets", [Topic("orderpubsub", "order-tickets")] (OrderTicket orderTicket, IOrderService orderService) => {
    Console.WriteLine("Subscriber received : " + orderTicket.OrderId);
    orderService.HandleOrderTicket(orderTicket);
    return Results.Ok();
});

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
