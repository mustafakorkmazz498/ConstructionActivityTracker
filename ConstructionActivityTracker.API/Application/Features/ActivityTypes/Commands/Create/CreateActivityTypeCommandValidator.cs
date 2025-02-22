using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Commands.Create;

public class CreateActivityTypeCommandValidator : AbstractValidator<CreateActivityTypeCommand>
{
    public CreateActivityTypeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
    }
}
