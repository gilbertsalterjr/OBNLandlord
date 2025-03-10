﻿using Application.Models.Wrapper;
using FluentValidation;
using MediatR;

namespace Application.Pipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidateMe
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task
                    .WhenAll(_validators.Select(vr => vr.ValidateAsync(context, cancellationToken)));

                if (!validationResults.Any(vr => vr.IsValid))
                {
                    List<string> errors = [];
                    
                    var failures = validationResults
                        .SelectMany(vr => vr.Errors)
                        .Where(f => f != null)
                        .ToList();

                    foreach (var failure in failures)
                    {
                        errors.Add(failure.ErrorMessage);
                    }

                    return (TResponse) await ResponseWrapper.FailAsync(errors);
                }
            }

            return await next();
        }
    }
}
