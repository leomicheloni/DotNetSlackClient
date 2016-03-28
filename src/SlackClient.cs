using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSlackClient
{
    /// <summary>
    /// Sends messages to Slack using this recomendation
    /// https://api.slack.com/incoming-webhooks
    /// </summary>
    public class SlackClient
    {
        /// <summary>
        /// The hook URL somehing like https://hooks.slack.com/services/SSSSSSST/KKKKSSSKKKSS/SKuKiKt7S6SjSeKK
        /// In order to use that you should allow incoming webhooks to your channel
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// Sends webhook message to Slack channel (if it is configured correctly)
        /// For simplicity it uses the default hook channel and name
        /// </summary>
        /// <param name="message">The message to send</param>
        public void NotifySlack(string message)
        {
            var request = (HttpWebRequest)WebRequest.Create(this.WebHookUrl);

            request.Method = "POST";
            request.ContentType = "application/json";
            
            var bytes = Encoding.UTF8.GetBytes("{\"text\":\"" + message  + "\"}");

            request.ContentLength = bytes.Length;

            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
        }
    }
}
