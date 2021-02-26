using System.Text.Json;

namespace TheRockPaperScissors.Client.Services
{
    public class Serialization<T>
    {
        public string Serialize(T o)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Serialize(o, options);
        }
    }
}
