using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingOnline.Controllers;
using ShoppingOnline.Models;
using ShoppingOnline.Models.Orders;
using ShoppingOnline.Models.Carts;


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
builder.Services.AddScoped<Utils>();

// Using Identity
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
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

// add swagger
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ToDo API",
            Description = "An ASP.NET Core Web API for managing ToDo items",
            
            TermsOfService = new Uri("https://example.com/terms"),
            
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        });
    } 
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Setup for web-api-javascript
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Sử dụng với phần Cart

app.UseAuthorization();
app.UseAuthentication(); // Using with Identity

// Use Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "ShoppingOnline v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductManagement}/{action=Index}/{id?}");

app.Run();