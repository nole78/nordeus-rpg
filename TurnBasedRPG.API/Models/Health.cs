namespace TurnBasedRPG.API.Models
{
    public class Health
    {
        public int MaxHealth { get; private set; } = 0;
        public int CurrentHealth { get; private set; } = 0;

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

        public bool isDead()
        {
            return CurrentHealth <= 0;
        }
    }
}
