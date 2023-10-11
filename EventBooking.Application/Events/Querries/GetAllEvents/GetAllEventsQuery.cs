using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetAllEvents;

public record GetAllEventsQuery() : IRequest<ErrorOr<List<EventDto>>>;

