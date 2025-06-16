using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessMS.Users.Domain.Otp;
using Store.BusinessMS.Users.Infrastructure.Database;
using Store.BusinessMS.Users.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Infrastructure.Repositories;

namespace Store.BusinessMS.Users.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(
                   connectionString,
                   sqlServerOptionsAction: sqlOptions =>
                   {
                       sqlOptions.EnableRetryOnFailure();
                       sqlOptions.MigrationsAssembly(typeof(StoreDbContext).Assembly.FullName);
                   });
            });

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<Otp>), typeof(GenericRepository<Otp, StoreDbContext>));
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
