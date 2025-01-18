using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace MesaService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : System.Configuration.Install.Installer
    {
        public ServiceInstaller()
        {
            InitializeComponent();
            if (Program.InternalEdition)
            {
                serviceInstaller1.ServiceName = "MesaServiceInternalEdition";
                serviceInstaller1.DisplayName = "MesaService (Internal Edition)";
            }
        }
    }
}
