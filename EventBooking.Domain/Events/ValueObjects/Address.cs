using System;
using EventBooking.Domain.Common.Models;

namespace EventBooking.Domain.Events.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string Number { get; private set; }

        public Address() { }

        public Address(string street, string state, string country, string number)
        {
            Street = street;
            State = state;
            Country = country;
            Number = number;
        }

        public static Address Create(string street, string state, string country, string number)
        {
            return new(street, state, country, number);
        }

        public void Update(string street, string state, string country, string number)
        {
            Street = street;
            State = state;
            Country = country;
            Number = number;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return State;
            yield return Country;
            yield return Number;
        }
    }
}

