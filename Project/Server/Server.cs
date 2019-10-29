/* Server.cs
 * authors: Merijn Hendriks, Amir "TheFuseGamer" Halloul
 * license: MIT License
 */

using EmuTarkov.Server.Http;
using EmuTarkov.Shared.Diagnostics;

namespace EmuTarkov.Server
{
	public class Server
    {
        public static ILogger Log;
        private HttpServer _httpServer;

        public Server(string address, ILogger log)
        {
            Log = log;
            Log.Info("Initializing server...");
            this._httpServer = new HttpServer(address);
        }

		public void Start()
		{
            Log.Info("Starting server...");
            this._httpServer.Start();
		}

		public void Stop()
		{
            Log.Info("Stopping server...");
            this._httpServer.Stop();
		}

        private string GetVersion() => Constants.version;
    }
}
