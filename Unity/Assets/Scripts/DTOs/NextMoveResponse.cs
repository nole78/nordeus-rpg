using Assets.Scripts.Models;
using NordeusRPG.Models;
using System.Collections.Generic;

namespace NordeusRPG.DTOs
{
    public class NextMoveResponse
    {
        public BattleState UpdatedState { get; set; }
        public List<CombatEvent> Events { get; set; }
    }
}
