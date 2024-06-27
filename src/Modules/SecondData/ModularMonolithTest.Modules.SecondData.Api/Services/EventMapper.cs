using ModularMonolithTest.Modules.SecondData.Api.Events;
using ModularMonolithTest.Shared.Abstractions.Kernel;
using ModularMonolithTest.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.Services
{
    public class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                SecondDataCreated => new Events.IntegrationEvents.SecondDataCreated("value"),
                _ => throw new ArgumentException(nameof(@event)),
            };

        public IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map)!;
    }
}
