using TurnBasedRPG.API.Domain.Models;

namespace TurnBasedRPG.API.Middleware
{
    public abstract class Validator<T> : IValidator<T>
    {
        private readonly List<Func<T, string?>> _rules = [];

        protected void Rule(Func<T,bool> predicate,string errorMessage)
        {
            _rules.Add(model => predicate(model) ? null : errorMessage);
        }

        public ValidationResult Validate(T model)
        {
            var result = new ValidationResult();

            foreach(var rule in _rules)
            {
                var error = rule(model);
                if (error != null)
                    result.AddError(error);
            }

            return result;
        }
    }
}
