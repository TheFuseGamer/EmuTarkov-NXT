/* Server.cs
 * authors: Merijn Hendriks, Amir "TheFuseGamer" Halloul
 * license: MIT License
 */

using System.IO;
using EmuTarkov.Server.Configuration;
using EmuTarkov.Server.Http;
using EmuTarkov.Shared.Diagnostics;

namespace EmuTarkov.Server
{
	public class Server
    {
        public static ILogger Log;
        private readonly HttpServer _httpServer;

        public Server(string address, ILogger log)
        {
            Log = log;
            Log.Info("Initializing server...");
            ConfigurationManager.Initialize(Path.Combine(Constants.ConfigFolder, Constants.ServerConfigFile), typeof(ServerConfiguration));
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
    }
}
