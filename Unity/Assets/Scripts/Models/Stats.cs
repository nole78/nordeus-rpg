using System;

namespace NordeusRPG.Models
{
    [Serializable]
    public class Stats
    {
        public int Attack { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int Magic { get; set; } = 1;
    }
}
