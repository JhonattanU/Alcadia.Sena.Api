using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alcadia.Sena.Api.Extensions
{
    public static class SerializationExtensions
    {
        private static Formatting Formatting => Formatting.None;

        private static JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };
            }
        }

        public static string Serialize<T>(this T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj, Formatting, Settings);
        }
    }
}
