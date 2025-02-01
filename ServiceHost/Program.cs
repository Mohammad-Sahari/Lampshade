using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _01_Framework.Application;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("LampshadeDb");
var services = builder.Services;


// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminPrivilege");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "ShopAccess");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Discount", "DiscountAccess");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "AccountAccess");

});


services.AddTransient<IFileUploader, FileUploader>();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
services.AddHttpContextAccessor();
services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPrivilege",
        builder => builder.RequireRole(new List<string> { Roles.Administrator,Roles.ContentAdmin }));

    options.AddPolicy("ShopAccess",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));
    
    options.AddPolicy("DiscountAccess",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));  
    
    options.AddPolicy("AccountAccess",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));

});


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

app.UseAuthentication();
app.UseCookiePolicy();
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
