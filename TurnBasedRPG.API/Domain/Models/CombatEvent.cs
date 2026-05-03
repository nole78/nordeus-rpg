using TurnBasedRPG.API.Domain.Enums;

namespace TurnBasedRPG.API.Domain.Models
{
    public class CombatEvent
    {
        public string MoveId { get; set; } = string.Empty;
        public string AttackerId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public MoveKind Kind { get; set; } = MoveKind.Damage;
        public int Value { get; set; } = 0;
        public bool IsSelf { get; set; } = false;
        public int ActionIndex { get; set; } = 0;
        public StatusEffect? AppliedEffect { get; set; }
    }
}
