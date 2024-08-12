using DAL_DataAccessLayer.Abstarct;
using DAL_DataAccessLayer.EntityFramework.Contexts;
using DAL_DataAccessLayer.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddDbContext<MyDbContext>();
            serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
            serviceCollection.AddScoped<IUserService,UserServiceManager>();
            serviceCollection.AddScoped<IProductService,ProductServiceManager>();
            return serviceCollection;
        }
    }
}
