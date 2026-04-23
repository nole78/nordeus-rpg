using NordeusRPG.Enums;

namespace NordeusRPG.Models
{
    public class StatusEffect
    {
        public EffectType Type { get; set; } = EffectType.BuffAttack;
        public int Value { get; set; } = 0;
        public int RemainingTurns { get; set; } = 0;
    }
}
