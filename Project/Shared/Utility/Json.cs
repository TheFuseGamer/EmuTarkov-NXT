/* Json.cs
 * authors: Merijn Hendriks, Amir "TheFuseGamer" Halloul
 * license: MIT License
 */

using Newtonsoft.Json;

namespace EmuTarkov.Shared.Utility
{
	public static class Json
	{
        public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        
    }
}
