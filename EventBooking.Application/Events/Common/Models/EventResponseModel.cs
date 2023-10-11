using System;
using EventBooking.Application.Common.Models.Users;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Application.Events.Common.Models
{
    public record EventResponseModel(Guid Id, string Title, string Description, EventDuration Duration, Address Address, string UserId, DateTime CreatedAt, DateTime? UpdatedAt, UserModel? User);
}

