using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageService
    {
        void Send(ICommand command);
        void Raise(IEvent eventMessage);
        Task<TResponseMessage> Request<TRequestMessage, TResponseMessage>(TRequestMessage message) 
            where TRequestMessage:class, IMessage 
            where TResponseMessage :class,IMessage ;
    }
}
