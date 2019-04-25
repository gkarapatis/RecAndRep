using log4net;
using Microsoft.AspNet.SignalR.Client;
using RecAndRep.Business.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WPFServer
{
    public abstract class HubConnectionHandler
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(HubConnectionHandler));

        protected abstract ApplicationRole Role { get; }
        IDictionary<string, string> HubConnectionParams = new Dictionary<string, string>();

        const string hubName = "ServerClientHub";
        const string ServerURI = "http://localhost:8080/signalr";
        
        protected string Name { get; set; }
        protected IHubProxy HubProxy { get; set; }
        protected HubConnection Connection { get; set; }

        protected abstract void IncomingEventBinding();

        public HubConnectionHandler(string name)
        {
            HubConnectionParams.Add("ApplicationRole", Role.ToString());
            HubConnectionParams.Add("UserName", name);
            
        }


        /// <summary>
        /// Creates and connects the hub connection and hub proxy.  
        /// </summary>
        public async Task<bool> ConnectAsync()
        {
            Connection = new HubConnection(ServerURI, HubConnectionParams);
            //Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy(hubName);

            //Handle incoming event from server: 
            IncomingEventBinding();

            for (int attempts = 0; attempts < 15; attempts++)           
            {
                try
                {
                    await Connection.Start();
                    return true;
                }
                catch
                {                    
                }
                Thread.Sleep(2);
            }
            return false;
        }

        protected  void Disconnect()
        {
            Connection.Stop();
            Connection.Dispose();
        }
    
    }
}
