using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Impl
{
    public class MessageHandlerDiscoveryService : IMessageHandlerDiscoveryService 
    {
        private readonly IMessageAssemblyConfiguration _messageConfiguration;

        public MessageHandlerDiscoveryService(IMessageAssemblyConfiguration messageConfiguration)
        {
            if (messageConfiguration == null) throw new ArgumentNullException(nameof(messageConfiguration));
            _messageConfiguration = messageConfiguration;
        }

        public ICollection<MessageHandlerInfo> GetEventHandler()
        {

            var handlerInfo = new List<MessageHandlerInfo>();
            foreach (var serviceEndpoint in _messageConfiguration.ServiceEndpoints)
            {
                var handlerInterfaceType = typeof(IEventHandler<>);

                var handlerTypes = serviceEndpoint.GetModules()
                    .SelectMany(m => m.GetTypes())
                    .Where(
                        t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType));

               

                foreach (var handlerType in handlerTypes)
                {
                    var handlerInterface =
                        handlerType.GetInterfaces()
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType);

                    foreach (var type in handlerInterface)
                    {
                        var messageType = type.GetGenericArguments().Single();
                        if (messageType.IsGenericParameter)
                            continue;

                        handlerInfo.Add(new MessageHandlerInfo
                        {
                            HandlerType = handlerType,
                            MessageType = messageType
                        });
                    }
                }
            }

           
            return handlerInfo;
        }

      

        public ICollection<MessageHandlerInfo> GetCommandHandler()
        {
            var handlerInfo = new List<MessageHandlerInfo>();
            foreach (var serviceEndpoint in _messageConfiguration.ServiceEndpoints)
            {
                var handlerInterfaceType = typeof(ICommandHandler<>);

                var handlerTypes = serviceEndpoint.GetModules()
                    .SelectMany(m => m.GetTypes())
                    .Where(
                        t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType));



                foreach (var handlerType in handlerTypes)
                {
                    var handlerInterface =
                        handlerType.GetInterfaces()
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType);

                    foreach (var type in handlerInterface)
                    {
                        var messageType = type.GetGenericArguments().Single();
                        if (messageType.IsGenericParameter)
                            continue;

                        handlerInfo.Add(new MessageHandlerInfo
                        {
                            HandlerType = handlerType,
                            MessageType = messageType
                        });
                    }
                }
            }


            return handlerInfo;
        }
    }
}
