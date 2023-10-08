using api_demo_products.Data;
using api_demo_products.Interfaces;
using api_demo_products.Logic;
using api_demo_products.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(ops =>
{
    ops.UseSqlServer(builder.Configuration.GetConnectionString("AzureSQL"));
}, contextLifetime: ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddScoped<IProductManager, ProductsManager>();
builder.Services.AddScoped<IProductRepository, ProductsRepository>();


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
