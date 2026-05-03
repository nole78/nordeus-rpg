using NordeusRPG.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class SaveData
    {
        public RunConfigResponse config;
        public PlayerData player;
        public List<string> defeatedEnemiesId;
    }
}
