using Microsoft.AspNetCore.Mvc;
using TurnBasedRPG.API.DTOs;

namespace TurnBasedRPG.API.Validators
{
    public static class NextMoveRequestValidator
    {
        public static bool Validate(NextMoveRequest req)
        {
            var state = req.CurrentState;
            return state.Hero != null &&
                   state.Enemy != null &&
                   state.Hero.Health != null &&
                   state.Enemy.Health != null;
        }
    }
}
