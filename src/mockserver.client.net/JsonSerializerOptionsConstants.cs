using System.Text.Json;
using System.Text.Json.Serialization;

namespace MockServer.Client.Net
{
    public static class JsonSerializerOptionsContants
    {
        public static JsonSerializerOptions Default => new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}