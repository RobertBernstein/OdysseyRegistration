
using Microsoft.EntityFrameworkCore;
using OdysseyMvc2024.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

builder.Services.AddScoped<IOdysseyEntities, OdysseyEntities>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdysseyEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OdysseyEntities")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

// TODO: Fix the potential null here.
app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();
