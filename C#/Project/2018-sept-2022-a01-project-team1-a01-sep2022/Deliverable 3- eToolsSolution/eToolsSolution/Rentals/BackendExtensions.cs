using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rentals.BLL;
using Rentals.DAL;

namespace Rentals
{
    public static class BackendExtensions
    {
        public static void RentalsBackendDependencies(this IServiceCollection services,
                                                      Action<DbContextOptionsBuilder> options
        )
        {
            services.AddDbContext<eTools2021Context>(options);


            //services from BLL
            services.AddTransient<CustomerServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new CustomerServices(context);
            });

            services.AddTransient<CouponServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new CouponServices(context);
            });

            services.AddTransient<RentalDetailServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new RentalDetailServices(context);
            });

            services.AddTransient<RentalEquipmentServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new RentalEquipmentServices(context);
            });

            services.AddTransient<RentalServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new RentalServices(context);
            });

            services.AddTransient<EmployeeServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new EmployeeServices(context);
            });

        }
    }
}