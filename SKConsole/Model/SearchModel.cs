using System.Text.Json.Serialization;
using System.Text.Json;

namespace SKConsole.Model
{
    public class SearchModel
    {
        [JsonPropertyName("value")]
        public JsonDocument? Value { get; set; }
    }
}
