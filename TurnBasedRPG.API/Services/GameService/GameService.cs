using System.Text.Json;
using System.Text.Json.Serialization;
using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Enums;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly IWebHostEnvironment _env;

        public GameService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public Result<RunConfigResponse> GenerateRunConfig()
        {
            var path = Path.Combine(_env.ContentRootPath, "Configuration", "gameConfig.json");

            if (!File.Exists(path))
                return Result<RunConfigResponse>.Failure("Game config file not found",ErrorType.Internal);

            try
            {
                var json = File.ReadAllText(path);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var config = JsonSerializer.Deserialize<RunConfigResponse>(json, options);

                if (config == null)
                    return Result<RunConfigResponse>.Failure("Failed to deserialize game config.", ErrorType.Internal);
                return Result<RunConfigResponse>.Success(config);
            }
            catch (JsonException ex)
            {
                return Result<RunConfigResponse>.Failure($"JSON error: {ex.Message}",ErrorType.Internal);
            }
        }
    }
}