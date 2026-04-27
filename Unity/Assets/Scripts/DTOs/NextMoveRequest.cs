using NordeusRPG.Models;

namespace NordeusRPG.DTOs
{
    public class NextMoveRequest
    {
        public BattleState CurrentState { get; set; }
        public string PlayerMove { get; set; } = string.Empty;
    }
}
