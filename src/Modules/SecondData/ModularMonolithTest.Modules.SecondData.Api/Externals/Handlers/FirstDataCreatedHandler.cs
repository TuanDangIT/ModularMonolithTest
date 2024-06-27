using ModularMonolithTest.Modules.FirstData.Messages.Events;
using ModularMonolithTest.Modules.SecondData.Api.DAL;
using ModularMonolithTest.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.Externals.Handlers
{
    //dla wspólnych tutaj event byłby z messages, ale tutaj będzie z lokalnego
    internal class FirstDataCreatedHandler : IEventHandler<Externals.FirstDataCreated>
    {
        private readonly ISecondDataDbContext _dbContext;

        public FirstDataCreatedHandler(ISecondDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(FirstDataCreated @event)
        {
            await _dbContext.FirstDatas.AddAsync(new Entities.FirstData()
            {
                Value = @event.Value,
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
