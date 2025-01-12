using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("LampshadeDb");
var services = builder.Services;


// Add services to the container.
builder.Services.AddRazorPages();

//Configure Modules
ShopManagementBootstrapper.Configure(services, connectionString);
DiscountManagementBootstrapper.Configure(services, connectionString);
InventoryManagementBootstrapper.Configure(services, connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
