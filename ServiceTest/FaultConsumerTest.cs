using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Example;
using MassTransit;

namespace ServiceTest
{
    public class FaultConsumerTest: IConsumer<Fault<CommandExample>>
    {
        public Task Consume(ConsumeContext<Fault<CommandExample>> context)
        {
             Console.Out.WriteLineAsync("Message Faulted");

            return context.CompleteTask;
        }
    }
}
