using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MassTransit;

namespace ServiceTest
{
    public class TestGenericFaultConsumer<TMessage> : IConsumer<Fault<TMessage>> where TMessage : class, IMessage
    {

        public Task Consume(ConsumeContext<Fault<TMessage>> context)
        {
            var message = context.Message.Message;

            Console.Out.WriteLineAsync("Generic Fault");

            return context.CompleteTask;
        }
    }
}
