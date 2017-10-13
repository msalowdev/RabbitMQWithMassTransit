using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Example;
using MassTransit;

namespace RabbitMQWithMassTransit
{
    public class TestMessage
    {
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(rmqConfig =>
            {
                var host = rmqConfig.Host(new Uri("rabbitmq://WIN-J42S4F2VOE7"), h =>
                {
                    h.Username("TestUser");
                    h.Password("test");
                    
                });

                rmqConfig.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.Handler<EventExample>(context =>
                    {
                        return Console.Out.WriteLineAsync(context.Message.Name);
                    });

                    ep.Handler<CommandExample>(context =>
                    {
                        return Console.Out.WriteLineAsync("Command received");
                    });
                });

            }

       )
           ;

            bus.Start();

            bool stop = false;

            while (!stop)
            {
                var text = Console.ReadLine();

                bus.Publish(new CommandExample
                {
                    Text = text,
                    Flag = stop
                });

                if (text.ToLower() == "exit")
                {
                    stop = true;
                }
            }
            bus.Stop();
        }
    }
}
