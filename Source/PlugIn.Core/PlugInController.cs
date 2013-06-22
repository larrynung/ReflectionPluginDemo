using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace PlugIn.Core
{
    public class PlugInController
    {
        private IHost _host;
        
        public IHost Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        public PlugInController(IHost host)
        {
            this.Host = host;
        }

        IModule GetModule(Assembly asm, string fullTypeName)
        {
            Type t = asm.GetType(fullTypeName);
            IModule module = null;
            if (!(t.IsNotPublic || t.IsAbstract))
            {
                object objInterface = t.GetInterface("IModule", true);

                if (objInterface != null)
                {
                    module = asm.CreateInstance(t.FullName) as IModule;
                    module.Host = Host;
                    return module;
                }
            }
            return null;
        }

        IModule GetModule(Assembly asm, Type moduleType)
        {
            return GetModule(asm, moduleType.FullName);
        }

        public IModule[] GetModules(Assembly asm)
        {
            List<IModule> modules = new List<IModule>();
            IModule module = null;
            foreach (Type t in asm.GetTypes())
            {
                module = GetModule(asm, t);

                if (module != null)
                    modules.Add(module);
            }
            return modules.ToArray ();
        }

        public IModule[] GetModules(string assemblyFile)
        {
            if (!File.Exists(assemblyFile))
                throw new FileNotFoundException();
            return GetModules(Assembly.LoadFile(assemblyFile));
        }

        public IModule[] GetModulesFromDirectory(string directoryPath)
        {
            List<IModule> modules = new List<IModule>();
            foreach (string file in Directory.GetFiles(directoryPath,"*.dll"))
                modules.AddRange (GetModules(file));
            return modules.ToArray ();
        }
    }
}
