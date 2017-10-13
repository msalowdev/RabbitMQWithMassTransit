using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Common.Impl
{
    public class MessageRequest : IMessageRequest
    {
        private readonly IBus _bus;

        public MessageRequest(IBus bus)
        {
            if (bus == null) throw new ArgumentNullException(nameof(bus));
            _bus = bus;
        }

        public async Task<TResponseMessage> SendRequest<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage, string address) where TRequestMessage : class, IMessage where TResponseMessage : class, IMessage
        {
            var requestTimeout = TimeSpan.FromSeconds(30);
            var uri = new Uri(address);
            var requestClient = new MessageRequestClient<TRequestMessage, TResponseMessage>(_bus, uri,
                requestTimeout);
            return await requestClient.Request(requestMessage, new CancellationToken());
        }
    }

}
