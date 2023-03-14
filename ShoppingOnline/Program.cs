using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Infrastructure;
using ShoppingOnline.Models;
using ShoppingOnline.Models.Orders;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=" +
                       Path.Combine(Directory.GetCurrentDirectory(), "Data\\mydb.db");
builder.Services.AddDbContext<AppDbContext>(optionsAction =>
    optionsAction.UseSqlite(connectionString)
);

builder.Services.AddScoped<IProductInterface, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<Cart>();

// Sử dụng với phần Cart
builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductManagement}/{action=Index}/{id?}");

app.Run();