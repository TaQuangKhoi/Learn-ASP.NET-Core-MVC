using CompanyManagement.Models;
using CompanyManagement.Models.Employees;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LocalDb");

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(optionsAction =>
    optionsAction.UseSqlServer(connectionString)
);
// builder.Services.AddScoped<AppDbContext>(provider => provider.GetService<AppDbContext>());
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=List}/{id?}");

app.Run();
