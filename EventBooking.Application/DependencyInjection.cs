using System;
using System.Reflection;
using EventBooking.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(typeof(DependencyInjection).Assembly);
        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return service;
    }
}

