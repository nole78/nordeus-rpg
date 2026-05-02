using TurnBasedRPG.API.Domain.Enums;
using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Services.EffectService
{
    public class EffectService : IEffectService
    {
        public StatusEffect? ApplyEffect(Character target, MoveEffect effect)
        {
            if (effect.StatusEffect == null)
                return null;
            
            var def = effect.StatusEffect;
            var existing = target.StatusEffects.FirstOrDefault(e => e.Type == def.Type);
            if (existing != null)
            {
                switch (def.Stacking)
                {
                    case StackingRule.Stack:
                        {
                            var newEffect = CreateEffect(def);
                            target.StatusEffects.Add(newEffect);
                            return newEffect;
                        }
                        ;
                    case StackingRule.Replace:
                        {
                            existing.Value = def.Value;
                            existing.RemainingTurns = def.Duration;
                            return existing;
                        }
                        ;
                    case StackingRule.RefreshDuration:
                        {
                            existing.RemainingTurns = def.Duration;
                            return existing;
                        }
                        ;
                }
                return null;
            }
            else
            {
                var newEffect = CreateEffect(def);
                target.StatusEffects.Add(newEffect);
                return newEffect;
            }
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
