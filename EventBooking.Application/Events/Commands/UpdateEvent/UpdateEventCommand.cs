using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using MediatR;

namespace EventBooking.Application.Events.Commands.UpdateEvent;
public record UpdateEventCommand(Guid Id, string Title, string Description, EventDurationModel Duration, AddressModel Address) : IRequest<ErrorOr<EventDto>>;
