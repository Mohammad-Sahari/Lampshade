    using DiscountManagement.Application;
using DiscountManagement.Application.Contarct.ColleagueDiscount;
using DiscountManagement.Application.Contarct.CustomerDiscount;
using DiscountManagement.domain.ColleagueDiscountAgg;
using DiscountManagement.domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {   
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
