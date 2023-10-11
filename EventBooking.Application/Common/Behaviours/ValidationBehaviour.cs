using System;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace EventBooking.Application.Common.Behaviours
{
    public class ValidationBehaviour<TResquest, TResponse> : IPipelineBehavior<TResquest, TResponse>
        where TResquest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TResquest> _validator;
        public ValidationBehaviour(IValidator<TResquest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TResquest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return await next();
            }
            var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
            return (dynamic)errors;
        }
    }
}

