using System;
namespace EventBooking.Infrastructure.Settings;

public class InfrastructureSetting
{
    public const string Name = "InfrastructureSetting";
    public string RedisServer { get; set; }
    public string RedisPort { get; set; }
    public string UserBaseUrl { get; set; }
}

