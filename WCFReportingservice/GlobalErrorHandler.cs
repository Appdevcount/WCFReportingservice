using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;


namespace WCFReportingservice
{
    class GlobalErrorHandler:System.ServiceModel.Dispatcher.IErrorHandler
    {

        public bool HandleError(Exception error)
        {
            return true;
            //throw new NotImplementedException();
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is FaultException)
                return;
            FaultException fe = new FaultException("A General service error has occured");
            System.ServiceModel.Channels.MessageFault mf = fe.CreateMessageFault();
            fault = Message.CreateMessage(version, mf, null);
            //throw new NotImplementedException();
        }
    }
}
