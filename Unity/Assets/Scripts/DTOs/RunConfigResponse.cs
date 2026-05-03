using Assets.Scripts.Models;
using NordeusRPG.Models;
using System.Collections.Generic;

namespace NordeusRPG.DTOs
{
    public class RunConfigResponse
    {
        public ProgressionConfig progressionConfig { get; set; }
        public Character Hero { get; set; }
        public List<Character> Enemies { get; set; } = new List<Character>();
    }
}
