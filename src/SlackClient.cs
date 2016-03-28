using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSlackClient
{
    class SlackClient
    {
        public string WebHookUrl { get; set; }
        public string BotName { get; set; }
        public string BotIcon { get; set; }
        public string MessageFormat { get; set; }
        public void NotifySlack(string message)
        {
            var client = new RestClient(WebHookUrl);
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(
                new
                {
                    text = message,
                    username = BotName,
                    icon_emoji = BotIcon,
                    mrkdwn = true
                });

            client.ExecuteAsync(request, null);
        }
    }
}
