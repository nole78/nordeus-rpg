using NordeusRPG.Enums;

namespace Assets.Scripts.Models
{
    public class CombatEvent
    {
        public string AttackerId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public MoveKind Kind { get; set; } = MoveKind.Damage;
        public int Value { get; set; } = 0; 
        public bool IsSelf { get; set; } = false;
        public int ActionIndex { get; set; } = 0;
    }
}
