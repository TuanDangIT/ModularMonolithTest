using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModularMonolithTest.Modules.SecondData.Api.DAL;
using ModularMonolithTest.Modules.SecondData.Api.Events;
using ModularMonolithTest.Modules.SecondData.Api.Services;
using ModularMonolithTest.Shared.Abstractions.Kernel;
using ModularMonolithTest.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.Controllers
{
    [ApiController]
    [Route("api/" + SecondDataModule.BasePath)]
    internal class SecondDataController : ControllerBase
    {
        private readonly ISecondDataDbContext _dbContext;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public SecondDataController(ISecondDataDbContext dbContext, IDomainEventDispatcher dispatcher, IEventMapper eventMapper, IMessageBroker messageBroker)
        {
            _dbContext = dbContext;
            _dispatcher = dispatcher;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }
        [HttpGet("test")]
        public async Task<ActionResult<string>> TestController()
        {
            await Task.Delay(100);
            return Ok("Second data controller is working!");
        }
        [HttpGet]
        public async Task<ActionResult<List<Entities.SecondData>>> GetAll()
        {
            var datas = await _dbContext.SecondDatas.ToListAsync();
            if (datas.Count is 0)
            {
                return Ok("List is empty");
            }
            return Ok(datas);
        }
        [HttpGet("firstdata")]
        public async Task<ActionResult<List<Entities.SecondData>>> GetAllFirstDataas()
        {
            var datas = await _dbContext.FirstDatas.ToListAsync();
            if (datas.Count is 0)
            {
                return Ok("List is empty");
            }
            return Ok(datas);
        }
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var data = await _dbContext.SecondDatas.AddAsync(new Entities.SecondData()
            {
                Value = "some value"
            });
            await _dbContext.SaveChangesAsync();
            var domainEvent = new SecondDataCreated("some value");
            await _dispatcher.DispatchAsync(domainEvent);
            var integrationEvent = _eventMapper.Map(domainEvent);
            await _messageBroker.PublishAsync(integrationEvent);
            return NoContent();
        }
        [HttpGet("thirddata")]
        public async Task<ActionResult<IEnumerable<Entities.OtherEntities.ThirdData>>> GetAllThirdDatas()
        {
            var results = await _dbContext.ThirdDatas.ToListAsync();
            return Ok(results);
        }
    }
}
