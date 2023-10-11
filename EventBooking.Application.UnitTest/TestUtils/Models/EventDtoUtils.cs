
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events.ValueObjects;


namespace EventBooking.Application.UnitTest.TestUtils.Models;

public static class EventDtoUtils
{
    public static EventDto CreateEventDto() => new EventDto(Constants.Constants.Event.Id, Constants.Constants.Event.Title, Constants.Constants.Event.Description, EventDurationUtils.CreateEventDuration(), AddressUtils.CreateEventAddress(), Constants.Constants.Event.UserId, Constants.Constants.Event.CreatedAt, Constants.Constants.Event.UpdatedAt);
}

