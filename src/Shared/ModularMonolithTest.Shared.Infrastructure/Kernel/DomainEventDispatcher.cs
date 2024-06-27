﻿using Microsoft.Extensions.DependencyInjection;
using ModularMonolithTest.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Shared.Infrastructure.Kernel
{
    internal sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync(params IDomainEvent[] events)
        {
            if (events is null || !events.Any())
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                var tasks = handlers.Select(x => (Task?)handlerType
                    .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, new[] { @event }));
                if(tasks.Any())
                {
                    await Task.WhenAll(tasks!);
                }
                else
                {
                    return;
                }
                
                //var tasks = handlers.Select(x => handlerType
                //    .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                //    ?.Invoke(x, new[] { @event }));
                //await Task.WhenAll((IEnumerable<Task>)tasks);
            }
        }
    }
}
