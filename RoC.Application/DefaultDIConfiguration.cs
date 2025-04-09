using Microsoft.Extensions.DependencyInjection;
using RoC.Application.Interfaces;
using RoC.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoC.Application
{
    public static class DefaultDIConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        { 
        services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
            return services;
        }
    }
}
