using TurnBasedRPG.API.Domain.Enums;
using TurnBasedRPG.API.Domain.Models;
using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Services.EffectService;
using TurnBasedRPG.API.Services.StatService;

namespace TurnBasedRPG.API.Services.CombatService
{
    public class CombatService : ICombatService
    {
        private readonly Random _random = new();
        private readonly IStatService _statService;
        private readonly IEffectService _effectService;

        public CombatService(IStatService statService, IEffectService effectService)
        {
            _statService = statService;
            _effectService = effectService;
        }

        public Result<NextMoveResponse> ProcessTurn(NextMoveRequest request)
        {
            var state = request.CurrentState;
            var events = new List<CombatEvent>();
            var hero = state.Hero;
            var enemy = state.Enemy;
            int actionIdx = 0;

            // Player move
            var move = hero.Moves.FirstOrDefault(m => m.Id == request.PlayerMove);
            if (move == null)
            {
                return Result<NextMoveResponse>.Failure("Invalid hero move", ErrorType.Validation);
            }
            PerformMove(hero, enemy, move, events, actionIdx++);


            // Enemy move
            if (!enemy.Health.IsDead())
            {
                var enemyMove = enemy.Moves[_random.Next(enemy.Moves.Count)];
                PerformMove(enemy, hero, enemyMove, events, actionIdx++);
            }

            _effectService.TickEffects(hero);
            _effectService.TickEffects(enemy);
            state.CurrentTurn++;
            // Return state
            return Result<NextMoveResponse>.Success(new NextMoveResponse { 
                UpdatedState = state ,
                Events = events
            });
        }

        private void PerformMove(Character attacker, Character defender, Move move, List<CombatEvent> events, int idx)
        {
            var (attack, magic, defense) = _statService.CalculateStats(attacker, defender);
            foreach (var effect in move.Effects)
            {
                var target = effect.Target == TargetType.Self ? attacker : defender;
                int amount = 0;
                StatusEffect? appliedEffect = null;
                switch (effect.Kind)
                {
                    case MoveKind.Damage:
                        amount = CalculateDamage(attack, magic, defense, effect);
                        target.Health.TakeDamage(amount);
                        break;

                    case MoveKind.Heal:
                        amount = CalculateHealing(attack, magic, effect);
                        target.Health.Heal(amount);
                        break;

                    case MoveKind.ApplyStatus:
                        appliedEffect = _effectService.ApplyEffect(target, effect);
                        break;
                }
                events.Add(new CombatEvent
                {
                    MoveId = move.Id,
                    AttackerId = attacker.Id,
                    TargetId = target.Id,
                    IsSelf = effect.Target == TargetType.Self ? true : false,
                    Kind = effect.Kind,
                    Value = amount,
                    ActionIndex = idx,
                    AppliedEffect = appliedEffect
                });
            }
        }

        private int CalculateHealing(int attack, int magic, MoveEffect effect)
        {
            int effectiveHeal = 0;

            switch (effect.ScalingStat)
            {
                case StatType.Attack:
                    {
                        effectiveHeal = attack;
                    }
                    ; break;
                case StatType.Magic:
                    {
                        effectiveHeal = magic;
                    }
                    ; break;
            }

            return effectiveHeal + effect.Value;
        }
        private int CalculateDamage(int attack, int magic, int defense, MoveEffect effect)
        {
            int effectiveAttack = 0;

            switch(effect.ScalingStat)
            {
                case StatType.Attack: 
                    {
                        effectiveAttack = attack;
                        effectiveAttack -= defense;
                        effectiveAttack = effectiveAttack <= 0 ? 0 : effectiveAttack;
                    }
                    ; break;
                case StatType.Magic:
                    {
                        effectiveAttack = magic;
                    }
                    ; break;
            }

            return effectiveAttack + effect.Value;
        }
    }
}
