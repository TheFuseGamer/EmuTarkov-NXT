using TestServer.Diagnostics;
using EmuTarkov.Server;

namespace TestServer
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Logger logger = new Logger();
            Server server = new Server("http://127.0.0.1:1337/", logger);
            server.Start();
        }
	}
}
