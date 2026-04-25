using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Services.CombatService
{
    public interface ICombatService
    {
        NextMoveResponse ProcessTurn(NextMoveRequest request);
    }
}
