using System;
using EventBooking.Application.Events.Commands.CreateEvent;
using EventBooking.Application.UnitTest.Events.Commands.TestUtils;
using EventBooking.Application.UnitTest.TestUtils.Constants;

namespace EventBooking.Application.UnitTest.Events.Commands.CreateEvent.TestUtils
{
    public static class CreateEventCommandUtils
    {
        public static CreateEventCommand CreateCommand() => new CreateEventCommand(Constants.Event.Title, Constants.Event.Description, CreateEventDurationUtils.CreateEventDuration(), CreateEventAddressUtils.CreateEventAddress());


    }
}

