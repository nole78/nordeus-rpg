namespace TurnBasedRPG.API.Domain.Models
{
    public class BattleState
    {
        public required Character Hero { get; set; }
        public required Character Enemy { get; set; }
        public int CurrentTurn { get; set; } = 1;
    }
}
