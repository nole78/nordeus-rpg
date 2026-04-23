using System.Collections.Generic;


namespace NordeusRPG.Models
{
    public class BattleState
    {
        public Character Hero { get; set; }
        public Character Enemy { get; set; }
        public List<StatusEffect> HeroStatusEffects { get; set; } = new List<StatusEffect>();
        public int TurnNumber { get; set; } = 1;
    }
}
