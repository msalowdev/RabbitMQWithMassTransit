using System;
using System.Linq;
using System.Runtime.InteropServices;
using Ninject;

namespace Common.Impl
{
    public class MessageHandlerFactory: IMessageHandlerFactory
    {
        private readonly IMessageHandlerDiscoveryService _discoveryService;
        private readonly IKernel _kernel;

        public MessageHandlerFactory(IMessageHandlerDiscoveryService discoveryService, IKernel kernel)
        {
            if (discoveryService == null) throw new ArgumentNullException(nameof(discoveryService));
            if (kernel == null) throw new ArgumentNullException(nameof(kernel));
            _discoveryService = discoveryService;
            _kernel = kernel;
        }

        public IEventHandler<TEvent> CreateEventHandler<TEvent>() where TEvent : IEvent
        {

            var eventType = typeof (TEvent);

            var handlers = _discoveryService.GetEventHandler();

            var handlerInfo = handlers.FirstOrDefault(i => i.MessageType == eventType);

            if(handlerInfo == null)
                throw new Exception($"Event handler not found. Event Type:[{eventType}],  Assembly:[{eventType.Assembly.FullName}]");

            Type[] typeArgs = { handlerInfo.MessageType};

            //var handlerToMake = handlerInfo.HandlerType.MakeGenericType(typeArgs);

            return _kernel.Get(handlerInfo.HandlerType) as IEventHandler<TEvent>;
                // Activator.CreateInstance(handlerToMake) as IEventHandler<TEvent>;
        }

        public ICommandHandler<TCommand> CreateCommandHandler<TCommand>() where TCommand : ICommand
        {
            var eventType = typeof(TCommand);

            var handlers = _discoveryService.GetCommandHandler();

            var handlerInfo = handlers.FirstOrDefault(i => i.MessageType == eventType);

            if (handlerInfo == null)
                throw new Exception($"Event handler not found. Event Type:[{eventType}],  Assembly:[{eventType.Assembly.FullName}]");

            Type[] typeArgs = { handlerInfo.MessageType };

            return _kernel.Get(handlerInfo.HandlerType) as ICommandHandler<TCommand>;

          
        }
    }
}
