using TurnBasedRPG.API.DTOs;
using TurnBasedRPG.API.Middleware;

namespace TurnBasedRPG.API.Validators
{
    public class NextMoveRequestValidator : Validator<NextMoveRequest>
    {
       public NextMoveRequestValidator()
        {
            Rule(x => x.CurrentState != null, "CurrentState is required");

            Rule(x => !string.IsNullOrWhiteSpace(x.PlayerMove), "PlayerMove is required");

            Rule(x => x.CurrentState?.Hero != null,"Hero is required");

            Rule(x => x.CurrentState?.Enemy != null,"Enemy is required");

            Rule(x => x.CurrentState?.Hero?.Health != null,"Hero health is required");

            Rule(x => x.CurrentState?.Enemy?.Health != null,"Enemy health is required");
        }
    }
}
