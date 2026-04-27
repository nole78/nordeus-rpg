using System;
using System.Collections.Generic;

namespace NordeusRPG.Models
{
    [Serializable]
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public Health Health { get; set; }
        public Stats BaseStats { get; set; }
        public List<Move> Moves { get; set; } = new List<Move>();
        public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();
    }
}
