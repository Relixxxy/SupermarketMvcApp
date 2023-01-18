using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Core.Service;
using SupermarketApp.Core.Service.Interfaces;
using SupermarketApp.Data.Context;
using SupermarketApp.Core.Models.ValidationRules;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository;
using SupermarketApp.Data.Repository.Interfaces;
using SupermarketApp.Core.Mapper;
using SupermarketApp.Core.Models;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IRepository<Manufacturer>, ManufacturerRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IValidator<DepartmentModel>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<ManufacturerModel>, ManufacturerValidator>();
builder.Services.AddScoped<IValidator<ProductModel>, ProductValidator>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(
    typeof(DepartmentProfile),
    typeof(ManufacturerProfile),
    typeof(ProductProfile));

builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();

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
