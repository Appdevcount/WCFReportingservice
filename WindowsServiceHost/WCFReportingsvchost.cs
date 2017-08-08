using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHost
{
    public partial class WCFReportingsvchost : ServiceBase
    {
        ServiceHost host;
        //Installed  the Windows service like below
        //C:\Windows\Microsoft.NET\Framework64\v4.0.30319>installutil -i "C:\Users\MScount\Documents\Visual Studio 2012\Visual Studio 2013\Projects\WCFReportingservice\WindowsServiceHost\bin\Debug\WindowsServiceHost.exe"
        //The service failed to start after installation - so given the Network services account permission to the service solution directory
        //Changed the logon type from Network services to system Logon Account/Siraj in service.msc after installing the windows service - Because service stop automaticall after starting- seen this suggestion in internet - The siraj account also has access to Database logon

        public WCFReportingsvchost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(typeof(WCFReportingservice.Reportingservice));
            host.Open();
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
