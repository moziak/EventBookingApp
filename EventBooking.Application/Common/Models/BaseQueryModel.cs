using System;
namespace EventBooking.Application.Common.Models
{
    public record BaseQueryModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

