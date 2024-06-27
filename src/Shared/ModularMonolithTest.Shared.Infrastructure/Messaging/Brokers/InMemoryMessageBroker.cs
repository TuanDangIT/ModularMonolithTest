using ModularMonolithTest.Shared.Abstractions.Messaging;
using ModularMonolithTest.Shared.Abstractions.Modules;
using ModularMonolithTest.Shared.Infrastructure.Messaging.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Shared.Infrastructure.Messaging.Brokers
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;

        public InMemoryMessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDispatcher)
        {
            _moduleClient = moduleClient;
            _asyncMessageDispatcher = asyncMessageDispatcher;
        }
        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var message in messages)
            {
                if(1 == 1)
                {
                    await _asyncMessageDispatcher.PublishAsync(message);
                    continue;
                }
                //tasks.Add(_moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}
