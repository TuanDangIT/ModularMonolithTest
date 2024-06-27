using ModularMonolithTest.Modules.FirstData.Core.DAL;
using ModularMonolithTest.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Core.Externals.Handlers
{
    internal class SecondDataCreatedHandler : IEventHandler<SecondDataCreated>
    {
        private readonly IFirstDataDbContext _dbContext;

        public SecondDataCreatedHandler(IFirstDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(SecondDataCreated @event)
        {
            await _dbContext.FourthDatas.AddAsync(new Entities.FourthData()
            {
                Value = @event.Value
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
