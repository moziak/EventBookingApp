using System;
using ErrorOr;
using EventBooking.Domain.Common.Models;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Domain.Events
{
    public class Event : AggregateRoot<Guid>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public EventDuration Duration { get; private set; }
        public Address Address { get; private set; }
        public string UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }



        private Event(Guid id, string title, string description, EventDuration duration, Address address, string userId, DateTime createdAt, DateTime? updatedAt) : base(id)
        {

            Title = title;
            UserId = userId;
            Duration = duration;
            Description = description;
            Address = address;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public static Event Create(string title, string description, EventDuration duration, Address address, string userId, DateTime createdAt)
        {
            return new(Guid.NewGuid(), title, description, duration, address, userId, createdAt, null);
        }

        public void Update(string title, string description)
        {
            Title = title;
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        private Event()
        {
        }
    }
}

