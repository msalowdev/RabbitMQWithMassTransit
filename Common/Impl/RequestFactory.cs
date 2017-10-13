using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Impl
{
    public class RequestFactory: IRequestFactory
    {
        public IMessageRequest CreateMessageRequest<TRequestMessage, TResponseMessage>()
        {
            throw new NotImplementedException();
           
        }
    }
}
