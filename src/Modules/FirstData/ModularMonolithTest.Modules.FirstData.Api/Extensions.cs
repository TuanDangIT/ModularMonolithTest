using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Modules.FirstData.Api.Controllers;
using ModularMonolithTest.Modules.FirstData.Core;
using ModularMonolithTest.Modules.FirstData.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ModularMonolithTest.Bootstrapper")]
namespace ModularMonolithTest.Modules.FirstData.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddFirstData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore(configuration);
            return services;
        }
    }
}
