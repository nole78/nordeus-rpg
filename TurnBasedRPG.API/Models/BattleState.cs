namespace TurnBasedRPG.API.Models
{
    public class BattleState
    {
        public Character Hero { get; set; }
        public Character Enemy { get; set; }
        public int CurrentTurn { get; set; } = 1;
    }
}
