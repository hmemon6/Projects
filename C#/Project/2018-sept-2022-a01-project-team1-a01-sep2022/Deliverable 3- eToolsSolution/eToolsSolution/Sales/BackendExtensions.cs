using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.BLL;
using Sales.DAL;

namespace Sales
{
    public static class BackendExtensions
    {
        public static void SalesBackendDependencies(this IServiceCollection services,
                                                     Action<DbContextOptionsBuilder> options
        )
        {
            services.AddDbContext<eTools2021Context>(options);

            services.AddTransient<SaleServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new SaleServices(context);
            });

            services.AddTransient<SaleDetailServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new SaleDetailServices(context);
            });

            services.AddTransient<CouponServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new CouponServices(context);
            });

            services.AddTransient<StockItemServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new StockItemServices(context);
            });

            services.AddTransient<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new CategoryServices(context);
            });

            services.AddTransient<RefundServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new RefundServices(context);
            });
        }
    }
}