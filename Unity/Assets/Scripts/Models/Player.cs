using NordeusRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Player
    {
        public string Username { get; set; } = string.Empty;
        public Character Hero { get; private set; }
        public LevelSystem LevelSystem { get; private set; }
        public List<Move> Moves { get; set; } = new List<Move>();

        public Player(Character character, ProgressionConfig config)
        {
            Hero = character;
            foreach (var move in character.Moves)
                Moves.Add(move);
            LevelSystem = new LevelSystem(config, this);
        }
    }
}
