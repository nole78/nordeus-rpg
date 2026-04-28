using System.ComponentModel.DataAnnotations;

namespace TurnBasedRPG.API.Domain.Models
{
    public class Health
    {
        [Required]
        public int MaxHealth { get; set; } = 0;
        [Required]
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
            {
                CurrentHealth = 0;
            }
        }

        public void Heal(int healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
    }
}
