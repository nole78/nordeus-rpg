using System.Collections.Generic;

namespace NordeusRPG.Models
{
    public class Move
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<MoveEffect> Effects { get; set; } = new List<MoveEffect>();
    }
}
