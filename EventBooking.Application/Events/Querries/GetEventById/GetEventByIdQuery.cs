using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetEventById;

public record GetEventByIdQuery(Guid Id) : IRequest<ErrorOr<EventResponseModel>>;


