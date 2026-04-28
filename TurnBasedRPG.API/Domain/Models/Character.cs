namespace TurnBasedRPG.API.Domain.Models
{
    public class Character
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public required Health Health { get; set; }
        public required Stats BaseStats { get; set; }
        public List<Move> Moves { get; set; } = [];
        public List<StatusEffect> StatusEffects { get; set; } = [];
    }
}
