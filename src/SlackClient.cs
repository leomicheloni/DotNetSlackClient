using System;
using System.Net;
using System.Text;

namespace DotNetSlackClient
{
    /// <summary>
    /// Sends messages to Slack using this recomendation
    /// https://api.slack.com/incoming-webhooks
    /// </summary>
    public class SlackClient
    {

        private string webHookUrl;

        /// <summary>
        /// The hook URL somehing like https://hooks.slack.com/services/SSSSSSST/KKKKSSSKKKSS/SKuKiKt7S6SjSeKK
        /// In order to use that you should allow incoming webhooks to your channel
        /// </summary>
        public string WebHookUrl {
            get
            {
                return webHookUrl;
            }

            set
            {
                Uri result;

                if(!Uri.TryCreate(value,UriKind.Absolute, out result)) throw new ArgumentException("WebHookUrl is not a valid URL");

                webHookUrl = value;
            }
        }

        /// <summary>
        /// Sends webhook message to Slack channel (if it is configured correctly)
        /// For simplicity it uses the default hook channel and name
        /// </summary>
        /// <param name="message">The message to send</param>
        public void NotifySlack(string message)
        {
            this.NotifySlack(message, string.Empty, string.Empty);
        }

        /// <summary>
        /// Sends webhook message to Slack channel (if it is configured correctly)
        /// For simplicity it uses the default hook channel and name
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <param name="username">Overrides default bot username</param>
        public void NotifySlack(string message, string username)
        {
            this.NotifySlack(message, username, string.Empty);
        }

        /// <summary>
        /// Sends webhook message to Slack channel (if it is configured correctly)
        /// For simplicity it uses the default hook channel and name
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <param name="username">Overrides default bot username</param>
        /// <param name="icon_emoji">Overrides default bot icon</param>
        public void NotifySlack(string message, string username, string icon)
        {
            if (string.IsNullOrWhiteSpace(this.WebHookUrl)) throw new ArgumentException("WebHookUrl is not a valid URL");

            var request = (HttpWebRequest)WebRequest.Create(this.WebHookUrl);

            request.Method = "POST";
            request.ContentType = "application/json";

            var bytes = Encoding.UTF8.GetBytes(this.GenerateJSON(message, username, icon));

            request.ContentLength = bytes.Length;

            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
        }

        private string GenerateJSON(string message, string username, string icon_emoji)
        {
            var result = string.Empty;

            if (!string.IsNullOrWhiteSpace(message))
            {
                result = "\"message\":\"" + message +"\"";
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                result += ",\"username\":\"" + username + "\"";
            }

            if (!string.IsNullOrWhiteSpace(icon_emoji))
            {
                result += ",\"icon_emoji\":\"" + icon_emoji +"\"";
            }

            result = string.Concat("{", result, "}");

            return result;
        }
    }
}
