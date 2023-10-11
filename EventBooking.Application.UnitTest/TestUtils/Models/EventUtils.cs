
using EventBooking.Domain.Events;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Application.UnitTest.TestUtils.Models;

public static class EventUtils
{
    public static Event CreateEvent() => Event.Create(Constants.Constants.Event.Title, Constants.Constants.Event.Description, CreateEventDuration(), CreateEventAddress(), Constants.Constants.Event.UserId, Constants.Constants.Event.CreatedAt);

    public static EventDuration CreateEventDuration() => EventDuration.Create(Constants.Constants.Event.DurationStartDate, Constants.Constants.Event.DurationEndDate, Constants.Constants.Event.DurationTimeZone);

    public static Address CreateEventAddress() => Address.Create(Constants.Constants.Event.AddressStreet, Constants.Constants.Event.AddressState, Constants.Constants.Event.AddressCountry, Constants.Constants.Event.AddressNumber);
}


