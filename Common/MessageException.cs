using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MessageException: Exception
    {
        public IMessage MessageContent { get; set; }

        public MessageException(string exceptionMessage, Exception ex, IMessage nessageContent) :base(exceptionMessage, ex)
        {
            MessageContent = nessageContent;
        }
    }
}
