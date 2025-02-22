﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Behaviors;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new ValidationContext<object>(request);
        IEnumerable<ValidationExceptionModel> enumerable = (from failure in _validators.Select((IValidator<TRequest> validator) => validator.Validate(context)).SelectMany((ValidationResult result) => result.Errors)
                                                            where failure != null
                                                            select failure).GroupBy((ValidationFailure p) => p.PropertyName, (string propertyName, IEnumerable<ValidationFailure> errors) => new ValidationExceptionModel
                                                            {
                                                                Property = propertyName,
                                                                Errors = errors.Select((ValidationFailure e) => e.ErrorMessage)
                                                            }).ToList();
        if (enumerable.Any())
        {
            throw new Application.CrossCuttingConcerns.Exceptions.ValidationException(enumerable);
        }

        return await next();
    }
}
