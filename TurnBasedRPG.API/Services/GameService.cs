using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Enums;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services
{
    public class GameService : IGameService
    {
        public RunConfigResponse GenerateRunConfig()
        {
            var hero = CreateHero();

            var enemies = new List<Character>
            {
                CreateEnemy("Goblin",50,6),
            };

            return new RunConfigResponse
            {
                Hero = hero,
                Enemies = enemies,
            };
        }


        private Character CreateHero()
        {
            return new Character
            {
                Name = "Hero",
                Health = new Health(100),
                BaseStats = new Stats
                {
                    Attack = 10,
                    Defense = 10,
                    Magic = 10
                },
                Moves = new List<Move>
                {
                    new Move
                    {
                        Name = "Slash",
                        Value = 10,
                        Type = MoveType.Physical
                    },
                    new Move
                    {
                        Name = "Fireball",
                        Value = 15,
                        Type = MoveType.Magic
                    }
                }
            };  
        }

        private Character CreateEnemy(string name, int hp, int attack)
        {
            return new Character
            {
                Name = name,
                Health = new Health(hp),
                BaseStats = new Stats
                {
                    Attack = attack,
                    Defense = 5,
                    Magic = 0
                },
                Moves = new List<Move>
                {
                    new Move
                    {
                        Name = "Claw",
                        Value = attack,
                        Type = MoveType.Physical
                    }
                }
            };
        }
    }
}
