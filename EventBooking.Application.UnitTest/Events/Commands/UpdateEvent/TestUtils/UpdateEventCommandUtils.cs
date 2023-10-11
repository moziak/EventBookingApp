using System;
using EventBooking.Application.Events.Commands.UpdateEvent;
using EventBooking.Application.UnitTest.Events.Commands.TestUtils;
using EventBooking.Application.UnitTest.TestUtils.Constants;

namespace EventBooking.Application.UnitTest.Events.Commands.UpdateEvent.TestUtils
{
    public static class UpdateEventCommandUtils
    {
        public static UpdateEventCommand UpdateCommand() => new(Constants.Event.Id, Constants.Event.Title, Constants.Event.Description, CreateEventDurationUtils.CreateEventDuration(), CreateEventAddressUtils.CreateEventAddress());
    }
}

