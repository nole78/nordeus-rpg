using NordeusRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class RunConfigResponse
    {
        public Character Hero { get; set; }
        public List<Character> Enemies { get; set; } = new List<Character>();
    }
}
