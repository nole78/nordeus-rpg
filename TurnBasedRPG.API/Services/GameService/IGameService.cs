using TurnBasedRPG.API.Domain.Models;
using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Services.GameService
{
    public interface IGameService
    {
        Result<RunConfigResponse> GenerateRunConfig();
    }
}
