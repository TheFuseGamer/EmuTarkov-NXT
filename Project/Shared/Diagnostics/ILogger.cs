using System;

namespace EmuTarkov.Shared.Diagnostics
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(Exception exception, string message = "");
    }

    public enum LogLevel
    {
        None,
        Debug,
        Info,
        Warning,
        Error
    }
}
