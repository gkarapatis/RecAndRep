using Microsoft.AspNet.SignalR.Client;
using RecAndRep.Business.Enums;
using System.Collections.Generic;
using WPFServer;

namespace RecAndRep.Server.Business
{
    public class ServerHubConnectionHandler : HubConnectionHandler
    {
        protected override ApplicationRole Role => ApplicationRole.Server;

        public ServerHubConnectionHandler(string name, ResponseMessage rmMethod, ClientCoupling ccMethod, ClientCoupling cdMethod) : base(name)
        {
            ResponseMessageMethod = rmMethod;
            ClientConnectedMethod = ccMethod;
            ClientDisconnectedMethod = cdMethod;
        }

        ResponseMessage ResponseMessageMethod;
        public delegate void ResponseMessage(string name, string key, string message);

        ClientCoupling ClientConnectedMethod;
        ClientCoupling ClientDisconnectedMethod;
        public delegate void ClientCoupling(string name);

        public void SendToClientUser(string clientUseName, string key, string message)
        {
            HubProxy.Invoke("SendToUser", clientUseName, key, message);
        }

        public void GetAvailableCustomers(string clientUseName, string message)
        {
            HubProxy.Invoke("GetAvailableCustomers");
        }

        protected override void IncomingEventBinding()
        {
            HubProxy.On<string, string, string>("ResponseMessage", (name, key, message) => ResponseMessageMethod(name, key, message));
            HubProxy.On<string>("ClientConnected", (name) => ClientConnectedMethod(name));
            HubProxy.On<string>("ClientDisconnected", (name) => ClientDisconnectedMethod(name));
        }
    }
}
