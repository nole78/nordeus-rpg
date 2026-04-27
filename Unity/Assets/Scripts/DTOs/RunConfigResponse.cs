using NordeusRPG.Models;
using System.Collections.Generic;

namespace NordeusRPG.DTOs
{
    public class RunConfigResponse
    {
        public Character Hero { get; set; }
        public List<Character> Enemies { get; set; } = new List<Character>();
    }
}
