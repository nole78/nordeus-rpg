using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class MoveEffect
    {
        public MoveKind Kind { get; set; } = MoveKind.Damage;
        public TargetType Target { get; set; } = TargetType.Enemy;
        public int Value { get; set; } = 0;
        public StatType ScalingStat { get; set; } = StatType.None;
        public EffectDefinition? StatusEffect { get; set; }
    }
}
