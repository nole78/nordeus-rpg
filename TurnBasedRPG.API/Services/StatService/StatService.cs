using TurnBasedRPG.API.Domain.Enums;
using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Services.StatService
{
    public class StatService : IStatService
    {
        public (int attack, int magic, int deffense) CalculateStats(Character attacker, Character defender)
        {
            int attack = attacker.BaseStats.Attack;
            int magic = attacker.BaseStats.Magic;
            int defense = defender.BaseStats.Defense;

            foreach (var effect in attacker.StatusEffects)
            {
                switch (effect.Type)
                {
                    case EffectType.BuffAttack:
                        {
                            attack += effect.Value;
                        }
                        ; break;
                    case EffectType.BuffMagic:
                        {
                            magic += effect.Value;
                        }
                        ; break;
                    case EffectType.DebuffAttack:
                        {
                            attack -= effect.Value;
                        }
                        ; break;
                    case EffectType.DebuffMagic:
                        {
                            magic -= effect.Value;
                        }
                        ; break;
                }
            }
            foreach (var effect in defender.StatusEffects)
            {
                switch (effect.Type)
                {
                    case EffectType.BuffDefense:
                        {
                            defense += effect.Value;
                        }
                        ; break;
                    case EffectType.DebuffDefense:
                        {
                            defense -= effect.Value;
                        }
                        ; break;
                }
            }
            attack = Math.Max(0, attack);
            magic = Math.Max(0, magic);
            defense = Math.Max(0, defense);

            return (attack, magic, defense);
        }
    }
}
