using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Modules.FirstData.Core;
using ModularMonolithTest.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Api
{
    internal class FirstDataModule : IModule
    {
        public const string BasePath = "firstdata";
        public string Name { get; } = "FirstData";
        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore(configuration);
        }

        public void Use(IApplicationBuilder app)
        {
            
        }
    }
}
