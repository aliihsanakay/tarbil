using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Gurcom.Notify.Services
{
  public  class WebServiceLayer
    {
        private static BildirimService.BKST_EXT_READ_SERVICE_PortTypeClient bildirimClient;
        public static BildirimService.BKST_EXT_READ_SERVICE_PortTypeClient BildirimClient
        {
            get
            {
                if(bildirimClient == null)
                {
                    var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                    binding.UseDefaultWebProxy = true;
                    var uri = new Uri("http://212.175.143.218:5555/ws/BKST_EXT.Services.BKST_EXT_READ_SERVICE/BKST_EXT_Services_BKST_EXT_READ_SERVICE_Port");                    
                    var endpoint = new EndpointAddress(uri);
                    binding.OpenTimeout = new TimeSpan(0,3, 0);
                    binding.CloseTimeout = new TimeSpan(0, 3, 0);
                    binding.SendTimeout = new TimeSpan(0, 3, 0);
                    binding.ReceiveTimeout = new TimeSpan(0, 3, 0);
                    //binding.MaxReceivedMessageSize = 1000000000000;
                    
                    bildirimClient = new BildirimService.BKST_EXT_READ_SERVICE_PortTypeClient(binding, endpoint);
                    
                    System.ServiceModel.Security.UserNamePasswordClientCredential credential = bildirimClient.ClientCredentials.UserName;
                    credential.UserName = "Gurkop";
                    credential.Password = "";
                }
                return bildirimClient;
            }
        }
    }
}
