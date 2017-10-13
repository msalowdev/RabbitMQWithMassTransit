using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Example
{
    public class EventExample: IEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
