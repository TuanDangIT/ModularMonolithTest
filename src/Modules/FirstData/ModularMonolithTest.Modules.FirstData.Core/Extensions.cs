using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Modules.FirstData.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ModularMonolithTest.Modules.FirstData.Api")]
namespace ModularMonolithTest.Modules.FirstData.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FirstDataDbContext>(builder =>
            {
                builder.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
            services.AddScoped<IFirstDataDbContext, FirstDataDbContext>();
            return services;
        }
    }
}
