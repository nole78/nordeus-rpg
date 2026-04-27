using System.ComponentModel.DataAnnotations;
using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.DTOs
{
    public class RunConfigResponse
    {
        public required Character Hero { get; set; }
        public List<Character> Enemies { get; set; } = new List<Character>();
    }
}
