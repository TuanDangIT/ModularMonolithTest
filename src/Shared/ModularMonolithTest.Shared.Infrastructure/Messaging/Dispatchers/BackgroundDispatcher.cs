﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModularMonolithTest.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class BackgroundDispatcher : BackgroundService
    {
        private readonly IMessageChannel _messageChannel;
        private readonly IModuleClient _moduleClient;
        private readonly ILogger<BackgroundDispatcher> _logger;

        public BackgroundDispatcher(IMessageChannel messageChannel, IModuleClient moduleClient,
            ILogger<BackgroundDispatcher> logger)
        {
            _messageChannel = messageChannel;
            _moduleClient = moduleClient;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Running the background dispatcher.");
            Console.WriteLine("123456");
            await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _moduleClient.PublishAsync(message);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
            }

            _logger.LogInformation("Finished running the background dispatcher.");
        }
    }
}
