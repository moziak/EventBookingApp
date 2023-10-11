using System;
using EventBooking.Domain.Common.Models;

namespace EventBooking.Domain.Events.ValueObjects
{
    public sealed class EventDuration : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string TimeZone { get; private set; } = default!;

        public EventDuration() { }

        private EventDuration(DateTime startDate, DateTime endDate, string timeZone)
        {
            StartDate = startDate;
            EndDate = endDate;
            TimeZone = timeZone;
        }

        public static EventDuration Create(DateTime startDate, DateTime endDate, string timeZone)
        {
            return new(startDate, endDate, timeZone);
        }
        public void Update(DateTime startDate, DateTime endDate, string timeZone)
        {
            StartDate = startDate;
            EndDate = endDate;
            TimeZone = timeZone;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
            yield return TimeZone;
        }

    }
}

