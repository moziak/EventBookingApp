using System;
using System.Net;

namespace EventBooking.Application.UnitTest.TestUtils.Constants;

public static partial class Constants
{
    public static class Event
    {
        public static Guid Id = Guid.NewGuid();
        public const string Title = "Event Title";
        public const string Description = "Event Description";
        public static DateTime DurationStartDate = DateTime.UtcNow;
        public static DateTime DurationEndDate = DateTime.UtcNow;
        public const string DurationTimeZone = "Event Duration Time Zone";
        public const string AddressStreet = "Event Address Street";
        public const string AddressState = "Event Address State";
        public const string AddressCountry = "Event Address Country";
        public const string AddressNumber = "Event Address Number";
        public const string UserId = "Event User Id";
        public static DateTime CreatedAt = DateTime.UtcNow;
        public static DateTime UpdatedAt = DateTime.UtcNow;

    }
}

