using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SKConsole.Model
{
    public class GetAzureSearch
    {
        [JsonPropertyName("SampleID")]
        public string? SampleID { get; set; }

        [JsonPropertyName("SampleQuestion")]
        public string? SampleQuestion { get; set; }

        [JsonPropertyName("SampleAnswer")]
        public string? SampleAnswer { get; set; }
    }
}
