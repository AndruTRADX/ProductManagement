using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProductManagement.Context;
using ProductManagement.Services;
using ProductManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create the configuration to add an in-memory DB
/* builder.Services.AddDbContext<ProductContext>(p => p.UseInMemoryDatabase("ProductsDB")); */

// Create the configuration to add a SQL database connection
var connectionString = builder.Configuration.GetConnectionString("cnProducts");

// Adding contexts
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));

// Injectors
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IUserService, UserServices>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
