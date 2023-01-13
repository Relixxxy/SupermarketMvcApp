using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Core.Service;
using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Context.SupermarketValidation;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository;
using SupermarketApp.Data.Repository.Interfaces;
using SupermarketApp.Data.Mapper;
using SupermarketApp.Data.Models;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddTransient<IRepository<Department>, DepartmentRepository>();
builder.Services.AddTransient<IRepository<Manufacturer>, ManufacturerRepository>();
builder.Services.AddTransient<IRepository<Product>, ProductRepository>();

builder.Services.AddTransient<IValidator<DepartmentModel>, DepartmentValidator>();
builder.Services.AddTransient<IValidator<Manufacturer>, ManufacturerValidator>();
builder.Services.AddTransient<IValidator<Product>, ProductValidator>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IManufacturerService, ManufacturerService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddAutoMapper(
    typeof(DepartmentProfile),
    typeof(ManufacturerProfile),
    typeof(ProductProfile));

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
