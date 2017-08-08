using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace WCFReportingservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportingservice" in both code and config file together.
    //[ServiceContract(Name = "IPublicReportingservice")]  //<wsdl:portType name="IPublicReportingservice"> - Port type refers the Contract name assigned in wsdl - make changes to wcf service without breaking clients

    [ServiceContract]
    //General reporting test
    public interface IPublicReportingservice
    {
        [OperationContract]
        string Getpublicinformation(string name);
    }
    [ServiceContract]
    public interface IPrivateReportingservice
    {

        [FaultContract(typeof(CustomFaultImplementation))]
        [OperationContract]
        string Getprivateinformation(string name);
    }
    //Service based on Employee details
    [ServiceContract]
    public interface IWCFEmployeeservice
    {
        [OperationContract]
        WCFEmployeeWOEF GetWCFEmployee(int Id);

        [OperationContract]
        void SaveWCFEmployee(WCFEmployeeWOEF Employee);

        [OperationContract]
        string UpdateWCFEmployee(WCFEmployeeWOEF Employee);
    }

    //Service based File handing task - File Download
    [ServiceContract]
    public interface IFileHandlingservice
    {
        [OperationContract]
        FileProp FileDownload();
    }

    //WCF RESTfull service(added assembly reference - System.ServiceModel.Web for using Restfull service attributes) CRUD using Entity framework
    [ServiceContract]
    public interface IWCFRESTEmployeeservice
    {
        [OperationContract]
        [WebInvoke(Method ="GET",
            //RequestFormat =WebMessageFormat.Xml,
            ResponseFormat =WebMessageFormat.Xml,
            UriTemplate = "empbyid/{Id}",
            BodyStyle =WebMessageBodyStyle.Wrapped)]
        WCFEmployee GetWCFRESTEmployee(int Id);

        [OperationContract]
        [WebInvoke(Method = "PUT", 
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "SaveWCFERESTmployee/{Id}",
            BodyStyle = WebMessageBodyStyle.Bare)]
        void SaveWCFERESTmployee(WCFEmployee Employee);

        [OperationContract]
        [WebInvoke(Method = "PUT", 
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "UpdateWCFRESTEmployee/{Id}",
            BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateWCFRESTEmployee(WCFEmployee Employee);

        [OperationContract]
        [WebInvoke(Method = "GET", 
            //RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "allemp",
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<WCFEmployee> ALLWCFRESTEmployee();

    }



}
