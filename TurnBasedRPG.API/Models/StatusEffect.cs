using TurnBasedRPG.API.Enums;

namespace TurnBasedRPG.API.Models
{
    public class StatusEffect
    {
        public EffectType Type { get; set; } = EffectType.BuffAttack;
        public int Value { get; set; } = 0;
        public int RemainintTurns { get; set; } = 0;
    }
}
