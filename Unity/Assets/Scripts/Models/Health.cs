using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordeusRPG.Models
{
    public class Health
    {
        public int Max { get; private set; } = 0;
        public int Current { get; private set; } = 0;

        public Health(int maxHealth)
        {
            Max = maxHealth;
            Current = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            if (Current < 0)
                Current = 0;
        }

        public void Heal(int healAmount)
        {
            Current += healAmount;
            if (Current > Max)
                Current = Max;
        }

        public bool isDead()
        {
            return Current <= 0;
        }
    }
}
