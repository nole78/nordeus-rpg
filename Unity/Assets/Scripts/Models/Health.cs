using System;

namespace NordeusRPG.Models
{
    [Serializable]
    public class Health
    {
        public int MaxHealth { get; set; } = 0;
        public int CurrentHealth { get; set; } = 0;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void Heal(int healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
    }
}
