# DotNetSlackClient
Simple Slack client written in C#

Available as [Nuget package](https://www.nuget.org/packages/DotNetSlackClient/) just do `` Install-Package DotNetSlackClient`` 

How to use that?

Just go to your Slack team => application => webhook, create a new one, get the URL and you're done

[Here's a link](https://api.slack.com/incoming-webhooks)

```csharp
    var client = new DotNetSlackClient.SlackClient("https://hooks.slack.com/services/T09EEEII/B00000K0T/AkARoLwrrwfPDIdwBTsUkP");
    client.NotifySlack("hola mundo");
``` 
Enjoy.
