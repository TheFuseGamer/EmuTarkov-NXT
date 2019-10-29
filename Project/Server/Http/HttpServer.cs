/* HttpRequestHandler.cs
 * authors: Amir "TheFuseGamer" Halloul
 * license: MIT License
 */

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using EmuTarkov.Server.Models;

namespace EmuTarkov.Server.Http
{
    public class HttpServer
    {
        public bool Running { get; set; }

        private readonly HttpListener _httpListener;
        private readonly HttpRequestHandler _httpHandler;
        private Thread _connectionThread;
        private bool _disposed;

        /// <summary>
        /// The HTTP server class which deals with responding to requests.
        /// </summary>
        /// <param name="prefix">HTTP server endpoint</param>
        public HttpServer(string prefix)
        {
            Server.Log.Debug("Initializing HTTP server...");
            this._httpListener = new HttpListener();
            this._httpHandler = new HttpRequestHandler();
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException(nameof(prefix), "Prefix can't be null!");
            if(this._httpListener.Prefixes.Contains(prefix))
                throw new Exception($"Prefix \"{prefix}\" is already bound!");
            if(!Uri.TryCreate(prefix, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttp)
                throw new Exception($"\"{prefix}\" is not a valid prefix!");
            this._httpListener.Prefixes.Add(prefix);
            Server.Log.Debug("HTTP server initialized.");
        }

        /// <summary>
        /// Starts the HTTP server
        /// </summary>
        public void Start()
        {
            if (!this._httpListener.IsListening)
            {
                this._httpListener.Start();
                this._connectionThread = new Thread(this.ConnectionThreadStart);
                this._connectionThread.Start();
                this.Running = true;
                Server.Log.Info($"HTTP server started listening on \"{this._httpListener.Prefixes.First()}\"");
            }
        }

        /// <summary>
        /// Stops the HTTP server
        /// </summary>
        public void Stop()
        {
            if (this._httpListener.IsListening)
            {
                this.Running = false;
                this._httpListener.Stop();
            }
        }

        /// <summary>
        /// Main HTTP server loop
        /// </summary>
        private void ConnectionThreadStart()
        {
            try
            {
                while (this.Running)
                {
                    HttpListenerContext context = _httpListener.GetContext();

                    string body = string.Empty;
                    if (context.Request.HasEntityBody)
                    {
                        MemoryStream ms = new MemoryStream();
                        context.Request.InputStream.CopyTo(ms);
                        byte[] data = ms.ToArray();
                        body = Ionic.Zlib.ZlibStream.UncompressString(data);
                    }

                    ClientRequest requestData = new ClientRequest()
                    {
                        Context = context,
                        //Client = Server.GetClient(context.Request.Cookies["PHPSESSID"]?.Value.ToLower()),
                        Body = body
                    };
                    _httpHandler.ProcessRequest(context.Request.Url.AbsolutePath, requestData);
                }
            }
            catch (HttpListenerException)
            {
                Console.WriteLine("HTTP server was shut down.");
            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed)
                return;

            if (disposing)
            {
                if (this.Running)
                    this.Stop();
                if (this._connectionThread != null)
                {
                    this._connectionThread.Abort();
                    this._connectionThread = null;
                }
            }
            this._disposed = true;
        }
    }
}
