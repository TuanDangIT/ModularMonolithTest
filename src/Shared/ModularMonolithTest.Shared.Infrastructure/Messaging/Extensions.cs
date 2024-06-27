using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Shared.Abstractions.Messaging;
using ModularMonolithTest.Shared.Infrastructure.Messaging.Brokers;
using ModularMonolithTest.Shared.Infrastructure.Messaging.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
            services.AddHostedService<BackgroundDispatcher>();
            return services;
        }
    }
}
