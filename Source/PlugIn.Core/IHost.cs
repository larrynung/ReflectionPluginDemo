using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugIn.Core
{
    public interface IHost
    {
        PlugInController PlugInAgent { get; }
    }
}
