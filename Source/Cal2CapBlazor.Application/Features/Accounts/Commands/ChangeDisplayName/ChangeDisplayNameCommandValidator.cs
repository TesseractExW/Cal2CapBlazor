using FluentValidation;
using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts;

public class ChangeDisplayNameCommandValidator : AbstractValidator<ChangeDisplayNameCommand>
{
    public ChangeDisplayNameCommandValidator()
    {
        RuleFor(e => e.AccountId)
            .NotEmpty()
            .WithMessage("Account ID is required");

        RuleFor(e => e.NewDisplayName)
            .NotEmpty()
            .WithMessage("Display name is required.")
            .Length(AccountConstants.MinimumDisplayNameLength, AccountConstants.MaximumDisplayNameLength)
            .WithMessage($"Display name must be between {AccountConstants.MinimumDisplayNameLength} and {AccountConstants.MaximumDisplayNameLength}");
    }
}
