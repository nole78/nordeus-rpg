using NordeusRPG.Enums;

namespace Assets.Scripts.Models
{
    public class CombatEvent
    {
        public string AttackerId;
        public string TargetId;

        public MoveKind Kind;

        public int Value; 

        public bool IsSelf;
    }
}
