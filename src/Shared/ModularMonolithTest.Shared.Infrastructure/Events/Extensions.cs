using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scrutor;

namespace ModularMonolithTest.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
                    //.WithoutAttribute<DecoratorAttribute>())
                //.AsImplementedInterfaces()
                //.WithScopedLifetime());
            return services;
        }
    }
}
