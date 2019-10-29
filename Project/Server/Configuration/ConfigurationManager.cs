using System;
using System.IO;
using EmuTarkov.Shared.Utility;

namespace EmuTarkov.Server.Configuration
{
    public static class ConfigurationManager
    {
        public static void Initialize(string path, Type type)
        {
            var configuration = Activator.CreateInstance(type);
            File.WriteAllText(path, Json.Serialize(configuration));
        }
    }
}
