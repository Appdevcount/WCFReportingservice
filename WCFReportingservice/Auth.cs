using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IdentityModel.Tokens;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace WCFReportingservice
{
    public class UserNamePassValidator : System.IdentityModel.Selectors.UserNamePasswordValidator //System.IdentityModel Added this assembly reference manually
    {
        public override void Validate(string userName, string password)
        {
            //try {
                if (string.IsNullOrEmpty(userName) | string.IsNullOrEmpty(password))
                { throw new ArgumentNullException(userName); }

                if (userName != "TEST" & password != "TEST")
                { throw new SecurityTokenException("Unknown Username or Password"); }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException("Unknown Username or Incorrect Password");
            //}

            //It is usernamepassword based Authentication
            //proxy.ClientCredentials.UserName.UserName = username;
            //proxy.ClientCredentials.UserName.Password = password;

        }
    }



    class AuthorizationPolicy : IAuthorizationPolicy
    {
        Guid _id = Guid.NewGuid();

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ClaimSet Issuer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // this method gets called after the authentication stage
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);
            // set the custom principal
            evaluationContext.Properties["Principal"] = new CustomPrincipal(client);
            return true;
        }
        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");
            IList <IIdentity > identities = obj as IList < IIdentity >;
            if (identities == null || identities.Count <= 0)
            throw new Exception("No Identity found");
            return identities[0];
        }
    }


    class CustomPrincipal : IPrincipal
    {
        public IIdentity _identity { get; set; }
        public string[] _roles { get; set; }
        public CustomPrincipal(IIdentity IIdentity)
        {

        }
        public IIdentity Identity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsInRole(string role)
        {
            if (_identity.Name == "test")
                _roles = new string[1] { "ADMIN" };
            else
                _roles = new string[1] { "USER" };
            return _roles.Contains(role);
            //throw new NotImplementedException();
        }
    }

}

//Security modes in a WCF service
//==================================

//In security mode WCF makes a secure communication channel, encrypting messages when communicating with clients.The following are the security modes.

//Message security mode: In this mode the message will be encrypted and pass over a non-secure channel so that nobody can read the message.

//Transport security mode: In this mode the communication channel will be encrypted and also provide integrity, privacy and so on.

//Mixed transfer security mode: This mode provides transport security for message privacy and it uses message security for secure credentials.

//Both security mode: This mode uses both transport and message security. So the message will be encrypted using message security and will pass over a secure channel using transport security.It provides more security than others but it degrades performance.

//Authentications in WCF service: In authentication process WCF verifies the caller (who calls the services) and checks whether they are authorized or not to get the service.

//Windows authentication: In this mode the caller must provide his/her Windows credential for authentication.

//Username/Password: In this mode the caller must provide username and password for authentication.

//X509 certificates: In this mode the caller must send certificate information and the service will check whether the certificate is valid.

//Custom mechanism: In this mode the user must use their own protocol and credentials type instead of built-in authentication.

//Issue token: In this mode both the caller and the service rely on a secure token service to issue the client a token for the service identity.It uses a Windows card space.

//No authentication: WCF service doesn't implement any authentication in this mode.



//    ==>Can apply the Authorization policy in Operation Contract method implementation by using PrincipalPermission Attribute as below
//[PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
//public string UpdatePatientData(PatientData PatientInfo)
//{
//    try
//    {
//        return string.Format("You entered: {0} , {1} , {2} , {3}",
//        PatientInfo.Name, PatientInfo.Age, PatientInfo.Gender, PatientInfo.Email);
//    }
//    catch (Exception exp)
//    {
//        MyFaultException theFault = new MyFaultException();
//        theFault.Reason = "Some Error " + exp.Message.ToString();
//        throw new FaultException<MyFaultException>(theFault);
//    }
//}
