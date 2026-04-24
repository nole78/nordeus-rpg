using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordeusRPG.Models
{
    public class Stats
    {
        public int Attack { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int Magic { get; set; } = 1;

        public Stats() { }
        public Stats(int attack, int defense, int magic)
        {
            Attack = attack;
            Defense = defense;
            Magic = magic;
        }
    }
}
