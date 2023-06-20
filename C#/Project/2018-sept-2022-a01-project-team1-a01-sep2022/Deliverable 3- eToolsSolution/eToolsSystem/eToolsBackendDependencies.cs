using eToolsSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eToolsSystem
{
    public static class eToolsBackendDependencies
    {

        public static void eToolsSystemBackendDependencies(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options
            )
        {
            //register the DbContext class with the service collection
            services.AddDbContext<eToolsContext>(options);


        }

    }
}