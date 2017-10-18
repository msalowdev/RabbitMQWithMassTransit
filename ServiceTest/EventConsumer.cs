using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Example;
using MassTransit;

namespace ServiceTest
{
    public class EventConsumer: IConsumer<EventExample>
    {
        public Task Consume(ConsumeContext<EventExample> context)
        {
            throw new NotImplementedException();
        }
    }
}
