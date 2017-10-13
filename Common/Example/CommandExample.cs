using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Example
{
    public class CommandExample : ICommand
    {
        public string Text { get; set; }
        public bool Flag { get; set; }
    }
}
