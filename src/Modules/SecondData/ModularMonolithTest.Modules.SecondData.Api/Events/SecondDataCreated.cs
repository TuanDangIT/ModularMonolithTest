using ModularMonolithTest.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.Events
{
    internal record class SecondDataCreated(string Value) : IDomainEvent;
}
