using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.DTOs
{
    public class NextMoveResponse
    {
        public required BattleState UpdatedState { get; set; }
    }
}
