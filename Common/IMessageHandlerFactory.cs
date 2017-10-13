using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageHandlerFactory
    {
        IEventHandler<TEvent> CreateEventHandler<TEvent>() where TEvent : IEvent;
        ICommandHandler<TCommand> CreateCommandHandler<TCommand>() where TCommand : ICommand;
    }
}
