using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PlugIn.Core;
using System.Windows.Forms;

namespace PlugIn.Module
{
    public class SubModule:IModule
    {
        private Form _view;
        private IHost _host;

        protected Form m_View 
        {
            get
            {
                if (_view == null)
                    _view = new MainForm() { MdiParent = Host as Form };
                return _view;
            }
        }


        #region IModule 成員

        public string Name
        {
            get
            {
                return "SubModule";
            }
        }

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

        public  void Execute()
        {            
            m_View.Show();
        }

        #endregion
    }
}
