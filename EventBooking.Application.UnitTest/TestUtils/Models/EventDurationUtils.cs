using System;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Application.UnitTest.TestUtils.Models;

public static class EventDurationUtils
{
    public static EventDuration CreateEventDuration() => EventDuration.Create(Constants.Constants.Event.DurationStartDate, Constants.Constants.Event.DurationEndDate, Constants.Constants.Event.DurationTimeZone);
}

