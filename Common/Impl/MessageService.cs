using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Common.Impl
{
    public class MessageService: IMessageService
    {
        private readonly IBus _bus;
        private readonly IRequestFactory _requestFactory;

        public MessageService(IBus bus, IRequestFactory requestFactory)
        {
            if (bus == null) throw new ArgumentNullException(nameof(bus));
            if (requestFactory == null) throw new ArgumentNullException(nameof(requestFactory));
            _bus = bus;
            _requestFactory = requestFactory;
        }

        public void Send(ICommand command)
        {
            try
            {
                _bus.Publish(command);
            }
            catch (Exception ex)
            {
                throw new MessageException("Failed to publish the command. See inner exception for more information", ex, command);
            }
            
        }

        public void Raise(IEvent eventMessage)
        {
            try
            {
                _bus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                throw new MessageException("Failed to publish the event. See inner exception for more information", ex,
                    eventMessage);
            }
        }

        public Task<TResponseMessage> Request<TRequestMessage, TResponseMessage>(TRequestMessage message) where TRequestMessage : class, IMessage where TResponseMessage : class, IMessage
        {
            var request = _requestFactory.CreateMessageRequest<TRequestMessage, TResponseMessage>();

            try
            {
                return request.SendRequest<TRequestMessage, TResponseMessage>(message, "rabbitmq://WIN-J42S4F2VOE7");
            }
            catch (Exception ex)
            {
                throw new MessageException("Failed to send the message request", ex, message);
            }
        }
    }
}
