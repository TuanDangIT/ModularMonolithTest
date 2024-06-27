using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Modules.SecondData.Api.DAL;
using ModularMonolithTest.Modules.SecondData.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddSecondData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SecondDataDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
            services.AddScoped<ISecondDataDbContext, SecondDataDbContext>();
            services.AddSingleton<IEventMapper, EventMapper>();
            return services;
        }
    }
}
