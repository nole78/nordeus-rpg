using System.Collections.Generic;


namespace NordeusRPG.Models
{
    public class BattleState
    {
        public Character Hero { get; set; }
        public Character Enemy { get; set; }
        public int TurnNumber { get; set; } = 1;
    }
}
