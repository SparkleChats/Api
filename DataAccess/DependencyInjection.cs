﻿using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(configuration
                .GetConnectionString("SqlServer") ?? throw new Exception("Connection does`nt exist")));

            services.AddScoped<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
