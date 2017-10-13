using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageAssemblyConfiguration 
    {
        IEnumerable<Assembly> ServiceEndpoints { get; } 
    }
}
