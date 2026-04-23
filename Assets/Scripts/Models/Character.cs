using System.Collections.Generic;

namespace NordeusRPG.Models
{
    [System.Serializable]
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public int MaxHealth { get; set; } = 100;
        public int CurrentHealth { get; set; } = 100;
        public int Attack { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int Magic { get; set; } = 1;
        public List<Move> Moves { get; set; } = new List<Move>();
        public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();
    }
}
