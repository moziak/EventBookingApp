using System;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Application.Events.Common.Models
{
    public record EventDto(Guid Id, string Title, string Description, EventDuration Duration, Address Address, string UserId, DateTime CreatedAt, DateTime? UpdatedAt);
}

