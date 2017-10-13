using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IEventHandler<in TEvent> where TEvent: IEvent
    {

        void Handle(TEvent eventMessage);
    }
}
