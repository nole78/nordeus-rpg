using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services.GameService
{
    public interface IGameService
    {
        Result<RunConfigResponse> GenerateRunConfig();
    }
}
