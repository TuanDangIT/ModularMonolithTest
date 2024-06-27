using ModularMonolithTest.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Messages.Events
{
    public record class FirstDataCreated(string Value) : IEvent;
}
