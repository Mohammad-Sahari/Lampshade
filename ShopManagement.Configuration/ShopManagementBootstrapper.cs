using _01_Framework.Infrastructure;
using _02_LampshadeQuery.Contract;
using _02_LampshadeQuery.Contract.Product;
using _02_LampshadeQuery.Contract.ProductCategory;
using _02_LampshadeQuery.Contract.Slide;
using _02_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            // Registering ProductCategory services in DI container
            services.AddTransient<IPorductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            // Registering Product services in DI container
            services.AddTransient<IProductApplication, ProductApplication >();
            services.AddTransient<IProductRepository, ProductRepository>();

            // Registering ProductPicture services in DI container
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            // Registering Slide services in DI container
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            // Registering query services in DI container                                                                                                      
            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            // Registering permission services in DI container
            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            // Registering CartCalculatorService services in DI container
            services.AddTransient<ICartCalculatorService, CartCalculateService>();

            // Registering DbContext in DI container
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
