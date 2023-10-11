using System;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Application.UnitTest.TestUtils.Constants;

namespace EventBooking.Application.UnitTest.Events.Commands.TestUtils;

public static class CreateEventAddressUtils
{
    public static AddressModel CreateEventAddress() => new(Constants.Event.AddressStreet, Constants.Event.AddressState, Constants.Event.AddressCountry, Constants.Event.AddressNumber);
}

