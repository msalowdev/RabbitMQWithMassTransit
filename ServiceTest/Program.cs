using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Example;
using Common.Impl;
using MassTransit;
using Ninject;
using IMessage = Common.IMessage;

namespace ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "rabbitmq://WIN-J42S4F2VOE7";

            var kernel = new StandardKernel();

            kernel.Bind<IMessageAssemblyConfiguration>().To<MessageAssemblyConfig>();
            kernel.Bind<IMessageHandlerFactory>().To<MessageHandlerFactory>();
            kernel.Bind<IMessageHandlerDiscoveryService>().To<MessageHandlerDiscoveryService>();

            kernel.Bind<IMessageService>().To<MessageService>();

            kernel.Bind<IRequestFactory>().To<RequestFactory>();

            kernel.Bind<CommandExampleConsumer>().ToSelf();
            
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri(address), h =>
                {
                    h.Username("TestUser");
                    h.Password("test");
                });

                config.ReceiveEndpoint(host, "Service_Queue", ep =>
                {

                    //ep.Consumer();
                    
                    //ep.LoadFrom(kernel);
                    ep.ConfigureConsumersForNinject(new[] {typeof (CommandExampleHandler).Assembly}, kernel);
                    //ep.Consumer(() => new FaultConsumerTest());
                    //ep.Handler<EventExample>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync(context.Message.Name);
                    //});
                    //ep.Handler<Fault<IMessage>>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync(context.Message.Message.ToString());
                    //});
                    //ep.Consumer(typeof(CommandExample), type => new CommandExampleConsumer(new MessageHandlerFactory(new MessageHandlerDiscoveryService(new MessageAssemblyConfig()))));

                    //ep.Handler<CommandExample>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync("Boo it came to this");
                    //});
                });
            });

            kernel.Bind<IBus>().ToConstant(bus);

            bus.Start();

            Console.WriteLine("Hit any key to quit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
