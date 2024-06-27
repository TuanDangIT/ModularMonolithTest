using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularMonolithTest.Shared.Abstractions.Modules;
using ModularMonolithTest.Shared.Infrastructure.Api;
using ModularMonolithTest.Shared.Infrastructure.Events;
using ModularMonolithTest.Shared.Infrastructure.Kernel;
using ModularMonolithTest.Shared.Infrastructure.Messaging;
using ModularMonolithTest.Shared.Infrastructure.Modules;
using ModularMonolithTest.Shared.Infrastructure.Services;

[assembly: InternalsVisibleTo("ModularMonolithTest.Bootstrapper")]
namespace ModularMonolithTest.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IList<Assembly> assemblies, IList<IModule> modules)
        {
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    //wyszukiwanie wszystkich części aplikacji, czyli FirstData.API itd., w których mamy kontrolery
                    foreach (var part in manager.ApplicationParts)
                    {
                        Console.WriteLine(part.Name);
                    }

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddEvents(assemblies);
            services.AddMessaging();
            services.AddModuleRequests(assemblies);
            services.AddDomainEvents(assemblies);
            services.AddHostedService<AppInitializer>();
            return services;
        }
        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.MapGet("api", () =>
            {
                return Results.Ok("ModularMonolithTest is working!");
            });
            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.MapControllers();
            app.MapControllers();
            return app;
        }
    }
}
