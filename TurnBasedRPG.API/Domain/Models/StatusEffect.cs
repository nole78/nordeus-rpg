using TurnBasedRPG.API.Domain.Enums;

namespace TurnBasedRPG.API.Domain.Models
{
    public class StatusEffect
    {
        public string Id { get; set; } = string.Empty;
        public EffectType Type { get; set; } = EffectType.BuffAttack;
        public int Value { get; set; } = 0;
        public bool SkipFirstTick { get; set; } = true;
        public int RemainingTurns { get; set; } = 0;
    }
}
