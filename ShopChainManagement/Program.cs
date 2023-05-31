using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopChainManagement;
using ShopChainManagement.Middlewares;
using ShopChainManagement.Services;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var services = builder.Services;

// Add services to the container.

services.AddControllersWithViews();

services.AddDbContext<ShopChainDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShopChainConnection")));

BaseServices.RegisterServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseMiddleware<HttpExceptionLoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ShopChainDbContext>();
await DbInitializer.Initialize(context);

app.Run();
