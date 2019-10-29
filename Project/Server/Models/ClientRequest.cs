using System.Net;
using EmuTarkov.Shared.Models;

namespace EmuTarkov.Server.Models
{
    public class ClientRequest
    {
        public HttpListenerContext Context { get; set; }
        public EftClient Client { get; set; }
        public string Body { get; set; }
    }
}
