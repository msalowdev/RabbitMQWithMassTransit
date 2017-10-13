using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageRequest
    {
        Task<TResponseMessage> SendRequest<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage, string address) where TRequestMessage: class, IMessage where TResponseMessage:class, IMessage;
    }
}
