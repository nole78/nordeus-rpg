using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Enums;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services
{
    public class CombatService : ICombatService
    {
        private readonly Random _random = new();

        public NextMoveResponse ProcessTurn(NextMoveRequest request)
        {
            var state = request.CurrentState;

            var hero = state.Hero;
            var enemy = state.Enemy;

            // Player move
            var move = hero.Moves.FirstOrDefault(m => m.Name.Equals(request.PlayerMove));
            if (move != null)
            {
                PerformMove(hero, enemy, move);
            }

            // Enemy move
            if(!enemy.Health.isDead())
            {
                var enemyMove = enemy.Moves[_random.Next(enemy.Moves.Count)];
                PerformMove(enemy, hero, enemyMove);
            }

            // Return state
            return new NextMoveResponse
            {
                UpdatedState = state
            };
        }

        private void PerformMove(Character attacker, Character defender, Move move)
        {
            switch(move.Type)
            {
                case MoveType.Heal:
                    {
                        attacker.Health.Heal(move.Value);
                    } break;
                case MoveType.Magic: 
                    {
                        defender.Health.TakeDamage(attacker.BaseStats.Magic + move.Value);
                    } break;
                case MoveType.Debuff: 
                    {
                        // TODO
                        //defender.StatusEffects.Add(new StatusEffect { })
                    } break;
                case MoveType.Physical: 
                    {
                        int damage = attacker.BaseStats.Attack + move.Value - defender.BaseStats.Defense;
                        defender.Health.TakeDamage(damage <= 0 ? 1 : damage);
                    } break;
                case MoveType.Buff: 
                    {
                        // TODO
                    } break;
            }
        }
    }
}
