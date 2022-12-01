using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWatchSystem.BLL;
using TrainWatchSystem.DAL;
using TrainWatchSystem.Entities;


namespace TrainWatchSystem
{
    public static class StartupExtension
    {
        public static void AddBackendDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            //  Within this method, we will register all the services that will
            //      be used by the system (context setup) and will be provided by
            //      the system.
            services.AddDbContext<TrainWatchContext>(options);

            //  register the service classes
            //  add any business logic layer class to the service collection so our 
            //      web app has access to the methods (services) within the BLL class
            //  The argument for the AddsTransient is called a factory
            //  Basically what you are adding is a localize method

            services.AddTransient<TrainWatchServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<TrainWatchContext>();
                //  create an instance of the service class (BuidVersionServices) supplying
                //      the context reference to the service class
                return new TrainWatchServices(context);
                //  return the service class instance
            }
            );

            services.AddTransient<RollingStockServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<TrainWatchContext>();
                //  create an instance of the service class (BuidVersionServices) supplying
                //      the context reference to the service class
                return new RollingStockServices(context);
                //  return the service class instance
            }
             );
        }
    }
}
