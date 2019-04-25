using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using RecAndRep.Business.Enums;
using RecAndRep.Utils.Collections;

namespace RecAndRep.Hubs
{

    public class ServerClientHub : Hub
    {
        string AdminGroup => "AdminGroup";

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }


        public void Send(string message)
        {
            clientMapper.TryGetBySecond(Context.ConnectionId, out var name);
            Clients.Group(AdminGroup).addMessage(name, message);
        }

        public void ResponseMessage(string key, string message)
        {
            clientMapper.TryGetBySecond(Context.ConnectionId, out var name);
            Clients.Group(AdminGroup).ResponseMessage(name, key, message);
        }


        public void SendToUser(string userName, string key, string message)
        {
            clientMapper.TryGetByFirst(userName, out var ConnectionId);
            if (ConnectionId != null)
            {
                Clients.Client(ConnectionId).addMessage(key, message);
            }
        }

        public void GetAvailableCustomers(string userName, string message)
        {
            Clients.Client(Context.ConnectionId).send(clientMapper.ToDictionary.Keys);
        }

        /// <summary>
        /// Bi-directional Dictionary
        /// Maps Client's UserName to ConnectionId (and vise versa)
        /// in order to keep Server Application agnostic of ConnectionId        
        /// </summary>
        /// https://stackoverflow.com/questions/268321/bidirectional-1-to-1-dictionary-in-c-sharp
        static BiDictionaryOneToOne<string, string> clientMapper = new BiDictionaryOneToOne<string, string>();

        public override Task OnConnected()
        {
            ApplicationRole.TryParse(Context.QueryString["ApplicationRole"], out ApplicationRole appRole);
            switch (appRole)
            {
                case ApplicationRole.Client:
                    clientMapper.Add(Context.QueryString["userName"], Context.ConnectionId);
                    Clients.Group(AdminGroup).ClientConnected(Context.QueryString["userName"]);
                    break;
                case ApplicationRole.Server:
                    Groups.Add(Context.ConnectionId, AdminGroup);
                    break;
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            clientMapper.TryGetBySecond(Context.ConnectionId, out var name);
            if (name != null)
                Clients.Group(AdminGroup).ClientDisconnected(Context.QueryString["userName"]);
            //todo: Clean users dictionary and send notification to server
            return base.OnDisconnected(stopCalled);
        }

    }





}
