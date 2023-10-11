using System;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Application.UnitTest.TestUtils.Constants;

namespace EventBooking.Application.UnitTest.Events.Commands.TestUtils;

public static class CreateEventDurationUtils
{
    public static EventDurationModel CreateEventDuration() => new(Constants.Event.DurationStartDate, Constants.Event.DurationEndDate, Constants.Event.DurationTimeZone);
}

