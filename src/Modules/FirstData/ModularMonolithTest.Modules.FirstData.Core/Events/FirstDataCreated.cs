using ModularMonolithTest.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Core.Events
{
    public record class FirstDataCreated(string Value) : IEvent;
}
