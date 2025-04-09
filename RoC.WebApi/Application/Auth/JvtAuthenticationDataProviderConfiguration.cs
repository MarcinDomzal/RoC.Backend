using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoC.Application.Interfaces;
using RoC.Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoC.WebApi.Application.Auth
{
    public static class JvtAuthenticationDataProviderConfiguration
    {
        public static IServiceCollection AddJwtAuthenticationDataProvider(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CookieSettings>(configuration.GetSection("CookieSettings"));
            services.AddScoped<IAuthenticationDataProvider, JwtAuthenticationDataProvider>();

            return services;
        }
    }
}
