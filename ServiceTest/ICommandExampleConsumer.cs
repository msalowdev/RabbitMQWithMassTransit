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
    public class CommandExampleConsumer : IConsumer<CommandExample>
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;

        public CommandExampleConsumer(IMessageHandlerFactory messageHandlerFactory)
        {
            if (messageHandlerFactory == null) throw new ArgumentNullException(nameof(messageHandlerFactory));
            _messageHandlerFactory = messageHandlerFactory;
        }

        public Task Consume(ConsumeContext<CommandExample> context)
        {
            var message = context.Message;
            var handler = _messageHandlerFactory.CreateCommandHandler<CommandExample>();
            handler.Execute(message);

            return Task.CompletedTask;
        }
    }
}
