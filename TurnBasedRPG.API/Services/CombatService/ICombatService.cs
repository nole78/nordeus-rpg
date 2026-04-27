using TurnBasedRPG.API.Domain.Models;
using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Services.CombatService
{
    public interface ICombatService
    {
        Result<NextMoveResponse> ProcessTurn(NextMoveRequest request);
    }
}
