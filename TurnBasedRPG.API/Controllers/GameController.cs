using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Services;

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
            var config = _gameService.GenerateRunConfig();
            return Ok(config);
        }
        [HttpPost("next-move")]
        public ActionResult<NextMoveResponse> NextMove([FromBody] NextMoveRequest request)
        {
            var result = _combatService.ProcessTurn(request);
            return Ok(result);
        }
    }
}
