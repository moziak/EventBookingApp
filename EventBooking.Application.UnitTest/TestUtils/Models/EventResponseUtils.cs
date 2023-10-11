using System;
using EventBooking.Application.Events.Common.Models;

namespace EventBooking.Application.UnitTest.TestUtils.Models
{
    public static class EventResponseUtils
    {
        public static EventResponseModel CreateEventResponseModel() => new EventResponseModel(Constants.Constants.Event.Id, Constants.Constants.Event.Title, Constants.Constants.Event.Description, EventDurationUtils.CreateEventDuration(), AddressUtils.CreateEventAddress(), Constants.Constants.Event.UserId, Constants.Constants.Event.CreatedAt, Constants.Constants.Event.UpdatedAt, UserModelUtils.CreateUserModel());
    }
}

