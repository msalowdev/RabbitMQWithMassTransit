using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Ninject;

namespace ServiceTest
{
    public static class ConfigForNinject
    {
        public static IRabbitMqReceiveEndpointConfigurator ConfigureConsumersForNinject(
       this IRabbitMqReceiveEndpointConfigurator ep, IEnumerable<Assembly> assembliesToScan, IKernel kernel)
        {
            //Scan all to get the consumers.
            var consumerInterface = typeof(IConsumer<>);
            foreach (var assembly in assembliesToScan)
            {
                var consumerTypes =
                    assembly.GetModules()
                        .SelectMany(m => m.GetTypes())
                        .Where(
                            t =>
                                t.GetInterfaces()
                                    .Any(i => i.IsGenericType && 
                                    !i.ContainsGenericParameters && 
                                    i.GetGenericTypeDefinition() == consumerInterface));

                foreach (var consumerType in consumerTypes)
                {
                    var consumerTypeInterface =
                        consumerType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == consumerInterface);


                    var messageType =
                    (consumerTypeInterface?.GenericTypeArguments)?.SingleOrDefault(i => typeof (IMessage).IsAssignableFrom(i));

                    var genericFaultHandlerType = typeof (TestGenericFaultConsumer<>);

                  

                  

                    if (!kernel.GetBindings(consumerType).Any())
                    {
                        kernel.Bind(consumerType).ToSelf();
                        
                    }
                    ep.Consumer(consumerType, type => kernel.Get(type));

                    if (messageType != null)
                    {
                        var faultHandler = genericFaultHandlerType.MakeGenericType(messageType);

                        if (!kernel.GetBindings(faultHandler).Any())
                        {
                            kernel.Bind(faultHandler).ToSelf();
                        }

                        ep.Consumer(faultHandler, type => kernel.Get(type));
                    }

                }

            }

            return ep;
        }
    }
}
