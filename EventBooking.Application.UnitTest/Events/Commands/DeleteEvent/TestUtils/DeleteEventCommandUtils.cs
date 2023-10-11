using System;
using EventBooking.Application.Events.Commands.DeleteEvent;
using EventBooking.Application.UnitTest.TestUtils.Constants;

namespace EventBooking.Application.UnitTest.Events.Commands.DeleteEvent.TestUtils;

public static class DeleteEventCommandUtils
{
    public static DeleteEventCommand DeleteCommand() => new(Constants.Event.Id);
}

