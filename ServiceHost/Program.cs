using System.Text.Encodings.Web;
using System.Text.Unicode;
using _01_Framework.Application;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("LampshadeDb");
var services = builder.Services;


// Add services to the container.
builder.Services.AddRazorPages();
services.AddTransient<IFileUploader, FileUploader>();
services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

//Configure Modules
ShopManagementBootstrapper.Configure(services, connectionString);
DiscountManagementBootstrapper.Configure(services, connectionString);
InventoryManagementBootstrapper.Configure(services, connectionString);
BlogManagementBootstrapper.Configure(services,connectionString);
CommentManagementBootstrapper.Configure(services, connectionString);
AccountManagementBootstrapper.Configure(services, connectionString);
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
