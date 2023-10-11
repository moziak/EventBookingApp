using System;
namespace EventBooking.Application.Common.Models.Users;



public record UserAddress
(
    string Street,
    string Suite,
    string City,
    string Zipcode,
    Geo Geo
);

public record Company
(
    string Name,
    string CatchPhrase,
    string Bs
);

public record Geo
(
    string Lat,
    string Lng
);

public record UserModel
(
    int? Id = default,
    string? Name = default,
    string? Username = default,
    string? Email = default,
    UserAddress? Address = default,
    string? Phone = default,
    string? Website = default,
    Company? Company = default
);

