using System;
using EmuTarkov.Shared.Diagnostics;

namespace TestServer.Diagnostics
{
    public class Logger : ILogger
    {
        public void Debug(string message)
        {
            Console.WriteLine("[DEBUG] " + message);
        }

        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void Warning(string message)
        {
            Console.WriteLine("[WARNING] " + message);
        }

        public void Error(Exception exception, string message = "")
        {
            Console.WriteLine("[ERROR] " + message + Environment.NewLine + exception.Message);
        }
    }
}
