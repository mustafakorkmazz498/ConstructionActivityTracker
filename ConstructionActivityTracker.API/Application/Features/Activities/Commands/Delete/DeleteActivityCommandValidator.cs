using FluentValidation;

namespace Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}