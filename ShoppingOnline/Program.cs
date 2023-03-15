using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Models;
using ShoppingOnline.Models.Orders;
using ShoppingOnline.Models.Carts;
using ShoppingOnline.Models.Users;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=" +
                       Path.Combine(Directory.GetCurrentDirectory(), "mydb.db");
builder.Services.AddDbContext<AppDbContext>(optionsAction =>
    optionsAction.UseSqlite(connectionString)
);

builder.Services.AddScoped<IProductInterface, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Using Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

/**
 * Removes the required attribute for non-nullable reference types.
 */
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


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
app.UseAuthentication(); // Using with Identity

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductManagement}/{action=Index}/{id?}");

app.Run();