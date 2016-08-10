using System;
using System.Configuration;
using System.Net;
using BC.EQCS.Models;
using RestSharp;

namespace BC.EQCS.Notifications
{
    public class NotificationSender
    {
        private string Address { get; set; }
        private string User { get; set; }
        private string Password { get; set; }
        private Guid SystemId { get;set; }
        private int ProductType { get; set; }

        public NotificationSender()
        { 
            Address = ConfigurationManager.AppSettings["GMSAddress"];
            User = ConfigurationManager.AppSettings["GMSUser"];
            Password = ConfigurationManager.AppSettings["GMSPassword"];
            SystemId =  new Guid(ConfigurationManager.AppSettings["SystemId"]);
            ProductType = Convert.ToInt32(ConfigurationManager.AppSettings["GMSProductType"]);
        }

        public void SendEmail(NotificationMessageModel cmd, out bool sent)
        {
            
            var client = new RestClient(Address)
            {
                Authenticator = new HttpBasicAuthenticator(User, Password)
            };

            var msg = new NotificationMessageSendModel()
            {
                CustomerGuid = Guid.NewGuid(),
                MessageType = "Email",
                ProductType = ProductType,
                Address = cmd.Recipient,
                Subject = cmd.Subject,
                MessageBody = cmd.Body + "<br /><br />This is an EQCS system-generated message. Please do not reply.<br />" +
                                "For EQCS help please email <a href='EQCSHelp@britishcouncil.org'>EQCSHelp@britishcouncil.org</a>",
                ActionedBy = SystemId,
                Category = "Notification"
            };

            //the api route is an external route does not come from the api project
            var request = new RestRequest("api/message/sendemail", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddObject(msg);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = string.Format("StatusCode: {0} \nContentLength: {1} \nErrorException: {2} \n" +
                    "ErrorMessage: {3} \nContent: \n{4}",
                    response.StatusCode, response.ContentLength, response.ErrorException,
                    response.ErrorMessage, response.Content);
               
              throw new Exception(error);
            }

            sent = true;
            //write to the db the sent email
        }

      
    }
 
}
