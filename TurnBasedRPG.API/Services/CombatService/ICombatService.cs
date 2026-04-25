using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services.CombatService
{
    public interface ICombatService
    {
        Result<NextMoveResponse> ProcessTurn(NextMoveRequest request);
    }
}
