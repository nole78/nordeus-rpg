using TurnBasedRPG.API.Enums;

namespace TurnBasedRPG.API.Models
{
    public class EffectDefinition
    {
        public EffectType Type { get; set; }
        public int Value { get; set; }
        public int Duration { get; set; }
    }
}
