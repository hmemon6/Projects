using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Purchasing.BLL;
using Purchasing.DAL;

namespace Purchasing
{
    public static class BackendExtensions
    {
        public static void PurchasingBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<eTools2021Context>(options);

            services.AddTransient<PurchaseOrderServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<eTools2021Context>();
                return new PurchaseOrderServices(context);
            });
        }
    }
}