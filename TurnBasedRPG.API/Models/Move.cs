using TurnBasedRPG.API.Enums;

namespace TurnBasedRPG.API.Models
{
    public class Move
    {
        public string Name { get; set; } = string.Empty;
        public MoveType Type { get; set; } = MoveType.Physical;
        public int Value { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
