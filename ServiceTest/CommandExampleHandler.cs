using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Example;

namespace ServiceTest
{
    public class CommandExampleHandler: ICommandHandler<CommandExample>
    {
        private readonly IMessageService _messageService;

        public CommandExampleHandler(IMessageService messageService)
        {
            if (messageService == null) throw new ArgumentNullException(nameof(messageService));
            _messageService = messageService;
        }

        public void Execute(CommandExample command)
        {
            Console.WriteLine("Message from command" + command.Text);

            _messageService.Raise(new EventExample
            {
                Id = DateTime.Now.Second,
                Name = DateTime.Now.ToLongDateString()
            });
        }
    }
}
