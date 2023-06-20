using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Receiving.BLL;
using Receiving.DAL;

namespace Receiving
{
    public static class BackendExtensions
    {
        public static void ReceivingBackendDependencies(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options
            )
        {
            //register the DbContext class with the service collection
            services.AddDbContext<ReceivingContext>(options);


            services.AddTransient<OutStandingPurchaseOrderServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<ReceivingContext>();
                return new OutStandingPurchaseOrderServices(context);
            });

            services.AddTransient<ReceiveOrderServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<ReceivingContext>();
                return new ReceiveOrderServices(context);
            });


        }
    }
}