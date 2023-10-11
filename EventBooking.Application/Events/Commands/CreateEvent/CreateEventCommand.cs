using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using MediatR;

namespace EventBooking.Application.Events.Commands.CreateEvent;

public record CreateEventCommand(string Title, string Description, EventDurationModel Duration, AddressModel Address) : IRequest<ErrorOr<EventDto>>;

