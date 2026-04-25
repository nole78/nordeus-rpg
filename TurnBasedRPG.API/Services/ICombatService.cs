using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Services
{
    public interface ICombatService
    {
        NextMoveResponse ProcessTurn(NextMoveRequest request);
    }
}
