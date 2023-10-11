using System;
using ErrorOr;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Common.Helpers;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetPaginatedEvents;

public record GetPaginatedEventsQuery() : EventFilterModel, IRequest<ErrorOr<PaginatedList<EventDto>>>;


