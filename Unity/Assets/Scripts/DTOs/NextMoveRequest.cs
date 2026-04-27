using NordeusRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class NextMoveRequest
    {
        public BattleState CurrentState { get; set; }
        public string PlayerMove { get; set; } = string.Empty;
    }
}
