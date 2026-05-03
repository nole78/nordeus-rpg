using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class LevelSystem
    {
        public int Level { get; set; } = 1;
        public int Experience { get; set; }

        private ProgressionConfig config;
        private Player player;

        public LevelSystem(ProgressionConfig config, Player player)
        {
            this.config = config;
            this.player = player;
        }

        public bool AddExperience(int amount)
        {
            Experience += amount;

            if (CanLevelUp())
            {
                LevelUp();
                return true;
            }
            return false;
        }

        private bool CanLevelUp()
        {
            if (Level - 1 >= config.expPerLevel.Count)
                return false;

            return Experience >= config.expPerLevel[Level - 1];
        }

        private void LevelUp()
        {
            Experience -= config.expPerLevel[Level - 1];
            Level++;

            ApplyLevelUpRewards();
        }

        private void ApplyLevelUpRewards()
        {
            // MVP: flat stat gain
            player.Hero.BaseStats.Attack += 1;
            player.Hero.BaseStats.Defense += 1;
            player.Hero.BaseStats.Magic += 1;

            player.Hero.Health.MaxHealth += 20;
            player.Hero.Health.CurrentHealth += 20;
        }
    }
}
