using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFReportingservice
{
    [DataContract]
    class CustomFaultImplementation
    {
        [DataMember]
        public string Error { get; set; }

        [DataMember]
        public string Details { get; set; }

    }
}
