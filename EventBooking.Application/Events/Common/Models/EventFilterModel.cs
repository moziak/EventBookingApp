using System;
using EventBooking.Application.Common.Models;

namespace EventBooking.Application.Events.Common.Models;

public record EventFilterModel : BaseQueryModel
{
    public string? Title { get; set; }
    public DateTime? EventStartDate { get; set; }
    public DateTime? EventEndDate { get; set; }
    public string? TimeZone { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}

