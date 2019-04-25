using Microsoft.AspNet.SignalR.Client;
using RecAndRep.Business.Enums;
using WPFServer;

namespace RecAndRep.Client.Business
{
    public class ClientHubConnectionHandler : HubConnectionHandler
    {
        protected override ApplicationRole Role => ApplicationRole.Client;

        public ClientHubConnectionHandler(string name, Publish method) : base(name)
        {
            PublishMethod = method;
        }

        Publish PublishMethod;
        public delegate void Publish(string name, string message);


        public void Response(string key, string message)
        {
            HubProxy.Invoke("ResponseMessage", key, message);
        }

        protected override void IncomingEventBinding()
        {
            HubProxy.On<string, string>("AddMessage", (name, message) => PublishMethod(name, message));
        }
    }
}
