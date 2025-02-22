using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Commands.Delete;

public class DeleteActivityTypeCommandValidator : AbstractValidator<DeleteActivityTypeCommand>
{
    public DeleteActivityTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
