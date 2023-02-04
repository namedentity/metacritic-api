using FluentValidation;
using MetacriticAPI.Contracts.Game;

namespace MetacriticAPI.Validation
{
    internal class GameSearchParametersValidator : AbstractValidator<GameQueryParameters>
    {
        internal GameSearchParametersValidator()
        {
            RuleFor(x => x.SearchTerm).NotEmpty();
        }
    }
}
