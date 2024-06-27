using ModularMonolithTest.Modules.SecondData.Api.DAL;
using ModularMonolithTest.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.Events.Handlers
{
    internal class SecondDataCreatedHandler : IDomainEventHandler<SecondDataCreated>
    {
        private readonly ISecondDataDbContext _dbContext;

        public SecondDataCreatedHandler(ISecondDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(SecondDataCreated @event)
        {
            await _dbContext.ThirdDatas.AddAsync(new Entities.OtherEntities.ThirdData()
            {
                Value = @event.Value
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
