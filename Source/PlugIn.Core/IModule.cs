using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugIn.Core
{
    public interface IModule
    {
        String Name { get; }
        IHost Host { get; set; }
        void Execute();
    }
}
