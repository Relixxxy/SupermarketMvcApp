using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Context.SupermarketValidation;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;
using SupermarketApp.Service;
using SupermarketApp.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<Department>), typeof(Repository<Department, SupermarketContext>));
builder.Services.AddScoped(typeof(IRepository<Manufacturer>), typeof(Repository<Manufacturer, SupermarketContext>));
builder.Services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product, SupermarketContext>));

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<Manufacturer>, ManufacturerValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}/{id?}");

app.Run();
