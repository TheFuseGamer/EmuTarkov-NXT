/* HttpRequestHandler.cs
 * authors: Amir "TheFuseGamer" Halloul
 * license: MIT License
 */

using System;
using System.Collections.Generic;
using EmuTarkov.Server.Models;

namespace EmuTarkov.Server.Http
{
    public class HttpRequestHandler
    {
        private readonly Dictionary<string, Action<ClientRequest>> _httpRequestHandlers;

        public HttpRequestHandler()
        {
            this._httpRequestHandlers = new Dictionary<string, Action<ClientRequest>>();
        }

        public void BindRequest(string path, Action<ClientRequest> action)
        {
            if (this._httpRequestHandlers.ContainsKey(path))
                this._httpRequestHandlers[path] = action;
            else
                this._httpRequestHandlers.Add(path, action);
        }

        public void ProcessRequest(string path, ClientRequest request)
        {
            Server.Log.Info($"Handling request [{path}]. body:\n{request.Body}");
            if(this._httpRequestHandlers.ContainsKey(path))
                this._httpRequestHandlers[path].Invoke(request);
            else
               UnhandledRequest(); 
        }

        private void UnhandledRequest()
        {
            throw new NotImplementedException();
        }
    }
}
