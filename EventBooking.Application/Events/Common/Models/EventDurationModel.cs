using System;
namespace EventBooking.Application.Events.Common.Models;

public record EventDurationModel(DateTime StartDate, DateTime EndDate, string TimeZone);

