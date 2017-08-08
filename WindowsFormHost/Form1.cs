using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormHost
{
    public partial class Form1 : Form
    {
        private ServiceHost host;

        public Form1()
        {
            InitializeComponent();
            host = new ServiceHost(typeof(WCFReportingservice.Reportingservice));
            host.Open();
            Startsvc.Enabled = false;
            Stopsvc.Enabled = true;
            svcstatus.Text = "WCF Reporting Service has Started";
            
        }

        private void Startsvc_Click(object sender, EventArgs e)
        {
            //Creating new instance of proxy class to access after closing again
            host = new ServiceHost(typeof(WCFReportingservice.Reportingservice));
            Startsvc.Enabled = false;
            Stopsvc.Enabled = true;
            svcstatus.Text = "WCF Reporting Service has Started";
            
        }

        private void Stopsvc_Click(object sender, EventArgs e)
        {
            host.Close();
            Startsvc.Enabled = true;
            Stopsvc.Enabled = false;
            svcstatus.Text = "WCF Reporting Service has Stopped";
            
        }

    }
}
