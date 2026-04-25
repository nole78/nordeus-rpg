using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Enums;
using TurnBasedRPG.API.Services.CombatService;
using TurnBasedRPG.API.Services.GameService;

namespace TurnBasedRPG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICombatService _combatService;

        public GameController(IGameService gameService, ICombatService combatService)
        {
            _gameService = gameService;
            _combatService = combatService;
        }

        [HttpGet("run-config")]
        public ActionResult<RunConfigResponse> GetRunConfig()
        {
            var result = _gameService.GenerateRunConfig();

            if(!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
            return Ok(result.Value);
        }
        [HttpPost("next-move")]
        public ActionResult<NextMoveResponse> NextMove([FromBody] NextMoveRequest request)
        {
            var result = _combatService.ProcessTurn(request);
            return Ok(result);
        }
    }
}
