namespace TurnBasedRPG.API.Domain.Models
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = [];
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
