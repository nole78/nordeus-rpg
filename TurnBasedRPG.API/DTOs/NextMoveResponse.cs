using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.DTOs
{
    public class NextMoveResponse
    {
        public required BattleState UpdatedState { get; set; }
        public required List<CombatEvent> Events { get; set; }
    }
}
