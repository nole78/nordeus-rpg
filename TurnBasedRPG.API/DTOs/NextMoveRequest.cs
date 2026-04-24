using System.ComponentModel.DataAnnotations;
using TurnBasedRPG.API.Models;

namespace TurnBasedRPG.API.DTOs
{
    public class NextMoveRequest
    {
        [Required]
        public BattleState CurrentState { get; set; } = default!;
        [Required(AllowEmptyStrings = false)]
        public string PlayerMove { get; set; } = string.Empty;
    }
}
