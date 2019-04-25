using log4net;
using Microsoft.Owin.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RecAndRep
{
    class SignalRServerManager
    {
        public static SignalRServerManager Instance = new SignalRServerManager();

        private readonly ILog log = LogManager.GetLogger(typeof(SignalRServerManager));
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        public void Start() => Task.Run(() => StartServer());

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                //todo: comment about server
                log.Warn($"A server is already running at {ServerURI}");
                return;
            }
            log.Warn($"erver started at  {ServerURI}");

        }
    }
}
