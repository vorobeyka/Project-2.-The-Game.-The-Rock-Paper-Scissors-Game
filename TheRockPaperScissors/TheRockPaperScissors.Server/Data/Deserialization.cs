using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Data
{
    public class Deserialization
    {
        public List<User> Users { get; set; }

        public string Path { get; set; }

        public Deserialization(string path)
        {
            Path = path;
            
        }
    }
}
