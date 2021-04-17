using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace webApiTokenAuthentication.Controllers
{
    public class NotifController : ApiController
    {
        

        [Authorize]
        [HttpGet]
        [Route("api/pushnotif")]
        public string PushNotif(string RegistrationID, string Message)
        {
            try
            {
                SendNotif(RegistrationID, Message);
                return "OK";
            }
            catch
            {
                return "Error";
            }
        }

        public void SendNotif(string RegistrationID, string Message)
        {
            try
            {

                string applicationID = "AAAAcTF6ab4:APA91bG-gdpX9SRpNcpr0Z3EMRHvPttTqAfzAXHfpMXG7ufx0aUjFN_RfbTbYHlirXvUuoZZrX-fHHMxXIccYkzneTmyx4ir4NZpdon-5OXv-w9aFxvUwmjRGEydHjIUuvoevo-jLB6x";

                string senderId = "486161410494";

                string deviceId = RegistrationID;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    //to = deviceId,
                    to = deviceId,
                    notification = new
                    {
                        body = Message,
                        title = "Bridge",
                        sound = "Enabled"

                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }   
        }
    
    }
}
