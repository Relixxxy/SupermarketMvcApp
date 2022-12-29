using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<Department>), typeof(Repository<Department, SupermarketContext>));
builder.Services.AddScoped(typeof(IRepository<Manufacturer>), typeof(Repository<Manufacturer, SupermarketContext>));
builder.Services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product, SupermarketContext>));

builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
