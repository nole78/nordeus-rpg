using TurnBasedRPG.API.Domain.Enums;
using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Services.EffectService
{
    public class EffectService : IEffectService
    {
        public bool ApplyEffect(Character target, MoveEffect effect)
        {
            if (effect.StatusEffect == null)
                return false;
            
            var def = effect.StatusEffect;
            var existing = target.StatusEffects.FirstOrDefault(e => e.Type == def.Type);
            if (existing != null)
            {
                switch (def.Stacking)
                {
                    case StackingRule.Stack:
                        {
                            target.StatusEffects.Add(CreateEffect(def));
                        }
                        ; break;
                    case StackingRule.Replace:
                        {
                            existing.Value = def.Value;
                            existing.RemainingTurns = def.Duration;
                        }
                        ; break;
                    case StackingRule.RefreshDuration:
                        {
                            existing.RemainingTurns = def.Duration;
                        }
                        ; break;
                }
            }
            else
                target.StatusEffects.Add(CreateEffect(def));
            return true;  
        }

        public StatusEffect CreateEffect(EffectDefinition def)
        {
            try
            {
                return new StatusEffect
                {
                    Type = def.Type,
                    Value = def.Value,
                    RemainingTurns = def.Duration,
                    SkipFirstTick = IsOffensiveEffect(def.Type)
                };
            }
            catch
            {
                return new StatusEffect();
            }
        }

        public bool TickEffects(Character character)
        {
            try
            {
                foreach (var effect in character.StatusEffects)
                {
                    if (effect.SkipFirstTick)
                    {
                        effect.SkipFirstTick = false;
                    }
                    else
                    {
                        effect.RemainingTurns--;
                    }
                }
                character.StatusEffects.RemoveAll(e => e.RemainingTurns <= 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool IsOffensiveEffect(EffectType type)
        {
            return type == EffectType.BuffAttack ||
                   type == EffectType.BuffMagic ||
                   type == EffectType.DebuffAttack ||
                   type == EffectType.DebuffMagic;
        }
    }
}
