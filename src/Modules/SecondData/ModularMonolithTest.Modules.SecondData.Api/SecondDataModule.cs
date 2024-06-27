using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Modules.SecondData.Api.DAL;
using ModularMonolithTest.Modules.SecondData.Api.Services;
using ModularMonolithTest.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api
{
    internal class SecondDataModule : IModule
    {
        public const string BasePath = "seconddata";
        public string Name { get; } = "SecondData";
        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SecondDataDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
            services.AddScoped<ISecondDataDbContext, SecondDataDbContext>();
            services.AddSingleton<IEventMapper, EventMapper>();
        }

        public void Use(IApplicationBuilder app)
        {
           
        }
    }
}
