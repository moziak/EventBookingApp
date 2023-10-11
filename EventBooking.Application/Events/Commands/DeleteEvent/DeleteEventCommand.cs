using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using MediatR;

namespace EventBooking.Application.Events.Commands.DeleteEvent;

public record DeleteEventCommand(Guid Id) : IRequest<ErrorOr<EventDto>>;

