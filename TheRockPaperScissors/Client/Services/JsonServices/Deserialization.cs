using System;
using System.IO;
using System.Text.Json;
using TheRockPaperScissors.Client.Exceptions;
using TheRockPaperScissors.Client.Options;

namespace TheRockPaperScissors.Client.Services
{
    public class Deserialization
    {
        public string Path { get; set; }

        public Uri BaseAddress { get; set; }

        public Deserialization(string path)
        {
            Path = path;

            try
            {
                var json = File.ReadAllText(Path);
                var settings = JsonSerializer.Deserialize<BaseAddress>(json);
                BaseAddress = new Uri(settings.Address);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException)
            {
                throw new DeserializationException(" Oops! Some problem with finding path to server.");
            }
        }
    } 
}
