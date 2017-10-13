using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServiceTest
{
    public class MessageAssemblyConfig : IMessageAssemblyConfiguration
    {
        public IEnumerable<Assembly> ServiceEndpoints => new[] {this.GetType().Assembly};

    }
}
