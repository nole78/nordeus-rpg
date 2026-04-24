namespace TurnBasedRPG.API.Models
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public Health Health { get; set; } = default!;
        public Stats BaseStats { get; set; } = new();
        public List<Move> Moves { get; set; } = [];
        public List<StatusEffect> StatusEffects { get; set; } = [];
    }
}
