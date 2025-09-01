using ComputerUse.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Infrastructure.Anthropic
{
    public static class Registration
    {
        public static IServiceCollection AddAnthropicInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IComputerActionTypesFactory, ComputerActionTypesFactory>();

            return services;
        }

    }
}
