using System;
using ErrorOr;

namespace EventBooking.Application.Events.Common.Errors
{
    public static class EventErrors
    {
        public static Error EventNotFound => Error.NotFound(
            code: "Event.NotFound",
            description: "Event not found");
    }
}

