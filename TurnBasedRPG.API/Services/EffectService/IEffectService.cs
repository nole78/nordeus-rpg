using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services.EffectService
{
    public interface IEffectService
    {
        bool ApplyEffect(Character target, MoveEffect effect);
        bool TickEffects(Character character);
        StatusEffect CreateEffect(EffectDefinition def);
    }
}
