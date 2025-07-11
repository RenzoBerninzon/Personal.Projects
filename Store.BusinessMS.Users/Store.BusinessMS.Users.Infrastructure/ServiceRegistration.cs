﻿using Microsoft.EntityFrameworkCore;
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
using Microsoft.AspNetCore.Identity;
using Store.BusinessMS.Users.Domain.User;

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

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<StoreDbContext>());
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreDbContext>().AddDefaultTokenProviders();

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<ApplicationUser>), typeof(GenericRepository<ApplicationUser, StoreDbContext>));
            services.AddTransient(typeof(IGenericRepository<Otp>), typeof(GenericRepository<Otp, StoreDbContext>));
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
