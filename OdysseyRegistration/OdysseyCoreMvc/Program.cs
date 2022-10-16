using Microsoft.EntityFrameworkCore;
using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// The database context is registered with the Dependency Injection container.
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
//
// The ASP.NET Core Configuration system reads the ConnectionString key. For local development, configuration gets the
// connection string from the appsettings.json file.
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
//
// When the app is deployed to a test or production server, an environment variable can be used to set the connection
// string to a test or production database server. For more information, see Configuration.
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
builder.Services.AddDbContext<OdysseyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OdysseyContext") ?? throw new InvalidOperationException("Connection string 'OdysseyContext' not found.")));

var app = builder.Build();

// Get a database context instance from the dependency injection (DI) container.
// Call the SeedData.Initialize method, passing to it the database context instance.
// Dispose the context when the seed method completes. The using statement ensures the context is disposed.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
