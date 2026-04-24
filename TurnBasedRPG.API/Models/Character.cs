namespace TurnBasedRPG.API.Models
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public Health Health { get; set; } = new Health(100);
        public Stats BaseStats { get; set; } = new Stats();
        public List<Move> Moves { get; set; } = new List<Move>();
        public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();
    }
}
