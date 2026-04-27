using Assets.Scripts.Enums;
using NordeusRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class EffectDefinition
    {
        public EffectType Type { get; set; } = EffectType.BuffAttack;
        public int Value { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public StackingRule Stacking { get; set; } = StackingRule.Replace;
    }
}
