using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Oazis.Domain.Helpers
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings SerializerSettings { get; }

        static JsonHelper()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            SerializerSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)) { AllowIntegerValues = false });
        }
    }
}
