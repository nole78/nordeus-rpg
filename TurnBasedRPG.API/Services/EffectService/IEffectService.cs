using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Services.EffectService
{
    public interface IEffectService
    {
        StatusEffect? ApplyEffect(Character target, MoveEffect effect);
        bool TickEffects(Character character);
        StatusEffect CreateEffect(EffectDefinition def);
    }
}
