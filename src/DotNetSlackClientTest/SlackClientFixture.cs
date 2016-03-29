using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetSlackClientTest
{
    [TestClass]
    public class SlackClientFixture
    {
        [TestMethod]
        public void Publish()
        {
            var client = new DotNetSlackClient.SlackClient();
            client.WebHookUrl = "https://hooks.slack.com/services/YOUR_ID";
            client.NotifySlack("hola mundo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionIsWebHookIsNotValidUrl()
        {
            var client = new DotNetSlackClient.SlackClient();
            client.WebHookUrl = "invalid!";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionIsWebHookIsNotValidUrlWhileNotify()
        {
            var client = new DotNetSlackClient.SlackClient();
            client.NotifySlack("message");
        }

    }
}
