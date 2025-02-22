using FluentValidation;

namespace Application.Features.Activities.Commands.Create;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(c => c.ActivityTypeId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.ActivityDate).NotEmpty();
    }
}
