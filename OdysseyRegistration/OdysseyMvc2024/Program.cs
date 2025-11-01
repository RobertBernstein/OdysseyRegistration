// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the main program for the entire application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using OdysseyMvc2024.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdysseyEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OdysseyConnection")));

// builder.Services.AddScoped<IOdysseyEntities, OdysseyEntities>();
builder.Services.AddScoped<IOdysseyRepository, OdysseyRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // serves from wwwroot

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

app.Run();
