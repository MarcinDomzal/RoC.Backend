using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RoC.Application.Interfaces;
using RoC.Application.Logic.Abstractions;
using RoC.Application.Services;
using RoC.Application.Validators;
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

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(BaseQueryHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

    }
}
