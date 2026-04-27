using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TurnBasedRPG.API.Domain.Enums;
using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Middleware;
using TurnBasedRPG.API.Services.CombatService;
using TurnBasedRPG.API.Services.GameService;
using TurnBasedRPG.API.Validators;

namespace TurnBasedRPG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICombatService _combatService;
        private readonly IValidator<NextMoveRequest> _validator;

        public GameController(IGameService gameService, ICombatService combatService,IValidator<NextMoveRequest> validator)
        {
            _gameService = gameService;
            _combatService = combatService;
            _validator = validator;
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
            var validation = _validator.Validate(request);

            if (!validation.IsValid)
                return BadRequest(new 
                {
                    title =  "One or more validation errors occurred.",
                    errors = validation.Errors 
                });

            var result = _combatService.ProcessTurn(request);
            if (!result.IsSuccess)
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
    }
}
