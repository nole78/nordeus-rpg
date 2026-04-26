using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Middleware
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T model);
    }
}
