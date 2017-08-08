using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehost
{
    class Program
    {
        static void Main()
        {
            using (System.ServiceModel.ServiceHost host = new System.ServiceModel.ServiceHost(typeof(WCFReportingservice.Reportingservice)))
            {
                //--------To configure service behavior ,contract ,binding and address in code instead of config file
                //host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled =true });
                //host.AddServiceEndpoint(typeof(WCFReportingservice.IPublicReportingservice), new BasicHttpBinding(), "Reportingservice");
                //host.AddServiceEndpoint(typeof(WCFReportingservice.IPrivateReportingservice), new NetTcpBinding(), "Reportingservice");
                //host.AddServiceEndpoint(typeof(WCFReportingservice.IWCFEmployeeservice), new BasicHttpBinding(), "WCFEmployeeservice");
                 
                //Throttling settings for the service in code .. can be done thru Config file also
                //System.ServiceModel.Description.ServiceThrottlingBehavior throttlingBehavior = new System.ServiceModel.Description.ServiceThrottlingBehavior
                //{
                //    MaxConcurrentCalls = 3,
                //    MaxConcurrentInstances = 3,
                //    MaxConcurrentSessions = 100
                //};
                //host.Description.Behaviors.Add(throttlingBehavior);

                host.Open();

                Console.WriteLine("Console WCF Host started @ " + DateTime.Now.ToString());

                Console.WriteLine(typeof(WCFReportingservice.FullTimeEmployee));

                Console.ReadLine();
            }
        }
    }
}
