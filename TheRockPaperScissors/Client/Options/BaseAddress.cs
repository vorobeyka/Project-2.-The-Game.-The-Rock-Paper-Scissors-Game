using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheRockPaperScissors.Client.Options
{
    public class BaseAddress
    {
        [JsonPropertyName("baseAddress")]
        public string Address { get; set; }
    }
}
