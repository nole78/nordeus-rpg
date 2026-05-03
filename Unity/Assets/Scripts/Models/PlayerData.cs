using NordeusRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int experience;
        public List<Move> moves;
        public List<Move> selectedMoves; 
    }
}
