using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Commands.Update;

public class UpdateActivityTypeCommandValidator : AbstractValidator<UpdateActivityTypeCommand>
{
    public UpdateActivityTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
    }
}
