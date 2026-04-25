using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services.StatService
{
    public interface IStatService
    {
        (int attack,int magic,int deffense) CalculateStats(Character attacker, Character defender);
    }
}
