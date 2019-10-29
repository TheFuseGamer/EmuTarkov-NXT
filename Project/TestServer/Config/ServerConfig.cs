using System;

namespace TestServer.Config
{
    [Serializable]
    public class ServerConfig
    {
        public string BackendUrl { get; set; } = "http://127.0.0.1:1337/";

        public ServerConfig()
        {
            // TODO: make it load from a file or create config file if it doesn't exist.
        }
    }
}
