using FluentValidation;

namespace Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ActivityTypeId).NotEmpty();
        RuleFor(c => c.ActivityDate).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
