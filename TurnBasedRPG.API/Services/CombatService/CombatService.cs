using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Enums;
using TurnBasedRPG.API.Models;
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
            var hero = state.Hero;
            var enemy = state.Enemy;

            // Player move
            var move = hero.Moves.FirstOrDefault(m => m.Id == request.PlayerMove);
            if (move == null)
            {
                return Result<NextMoveResponse>.Failure("Invalid hero move", ErrorType.Validation);
            }
            PerformMove(hero, enemy, move);


            // Enemy move
            if (!enemy.Health.IsDead())
            {
                var enemyMove = enemy.Moves[_random.Next(enemy.Moves.Count)];
                PerformMove(enemy, hero, enemyMove);
            }

            _effectService.TickEffects(hero);
            _effectService.TickEffects(enemy);
            // Return state
            return Result<NextMoveResponse>.Success(new NextMoveResponse { UpdatedState = state });
        }

        private void PerformMove(Character attacker, Character defender, Move move)
        {
            var (attack, magic, defense) = _statService.CalculateStats(attacker, defender);
            foreach (var effect in move.Effects)
            {
                var target = effect.Target == TargetType.Self ? attacker : defender;

                switch (effect.Kind)
                {
                    case MoveKind.Damage:
                        var dmg = CalculateDamage(attack, magic, defense, effect);
                        target.Health.TakeDamage(dmg);
                        break;

                    case MoveKind.Heal:
                        var heal = CalculateHealing(attack, magic, effect);
                        target.Health.Heal(heal);
                        break;

                    case MoveKind.ApplyStatus:
                        _effectService.ApplyEffect(target, effect);
                        break;
                }
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
