using System;
using EventBooking.Domain.Events.ValueObjects;

namespace EventBooking.Application.UnitTest.TestUtils.Models;

public static class AddressUtils
{
    public static Address CreateEventAddress() => Address.Create(Constants.Constants.Event.AddressStreet, Constants.Constants.Event.AddressState, Constants.Constants.Event.AddressCountry, Constants.Constants.Event.AddressNumber);
}

