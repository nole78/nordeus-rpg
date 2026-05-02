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
        public Character Hero { get; set; }
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
        public List<Move> Moves { get; set; } = new List<Move>();
    }
}
