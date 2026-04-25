using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Services
{
    public interface IGameService
    {
        RunConfigResponse GenerateRunConfig();
    }
}
