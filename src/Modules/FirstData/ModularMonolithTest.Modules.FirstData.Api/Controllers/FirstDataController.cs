using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModularMonolithTest.Modules.FirstData.Core.DAL;
using ModularMonolithTest.Modules.FirstData.Core.Events;
using ModularMonolithTest.Shared.Abstractions.Events;
using ModularMonolithTest.Shared.Abstractions.Messaging;
using ModularMonolithTest.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Api.Controllers
{
    [ApiController]
    [Route("api/" + FirstDataModule.BasePath)]
    internal class FirstDataController : ControllerBase
    {
        private readonly IFirstDataDbContext _dbContext;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMessageBroker _messageBroker;

        //private readonly IModuleClient _moduleClient;

        public FirstDataController(IFirstDataDbContext dbContext, IEventDispatcher eventDispatcher, IMessageBroker messageBroker/*IModuleClient moduleClient*/)
        {
            _dbContext = dbContext;
            _eventDispatcher = eventDispatcher;
            _messageBroker = messageBroker;
            //_moduleClient = moduleClient;
        }
        [HttpGet("test")]
        public ActionResult<string> TestController()
        {
            return Ok("FirstData controller is working!");
        }
        [HttpGet]
        public async Task<ActionResult<List<Core.Entities.FirstData>>> GetAll()
        {
            var datas = await _dbContext.FirstDatas.ToListAsync();
            if(datas.Count is 0)
            {
                return Ok("List is empty");
            }
            return Ok(datas);
        }
        [HttpGet("fourthdata")]
        public async Task<ActionResult<List<Core.Entities.FirstData>>> GetAllFourthData()
        {
            var datas = await _dbContext.FourthDatas.ToListAsync();
            if (datas.Count is 0)
            {
                return Ok("List is empty");
            }
            return Ok(datas);
        }
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            await _dbContext.FirstDatas.AddAsync(new Core.Entities.FirstData()
            {
                Value = "some value"
            });
            await _dbContext.SaveChangesAsync();
            //await _eventDispatcher.PublishAsync(new FirstDataCreated("some value"));
            //await _moduleClient.PublishAsync(new FirstDataCreated("some value"));
            await _messageBroker.PublishAsync(new FirstDataCreated("some value"));
            return NoContent();
        }
    }
}
