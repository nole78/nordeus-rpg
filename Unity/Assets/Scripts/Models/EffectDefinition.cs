using NordeusRPG.Enums;


namespace NordeusRPG.Models
{
    public class EffectDefinition
    {
        public EffectType Type { get; set; } = EffectType.BuffAttack;
        public int Value { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public StackingRule Stacking { get; set; } = StackingRule.Replace;
    }
}
