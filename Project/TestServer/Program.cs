using TestServer.Diagnostics;
using EmuTarkov.Server;
using TestServer.Config;

namespace TestServer
{
	public static class Program
	{
		public static void Main(string[] args)
		{
            ServerConfig config = new ServerConfig();
			Logger logger = new Logger();
            Server server = new Server(config.BackendUrl, logger);
            server.Start();
        }
	}
}
