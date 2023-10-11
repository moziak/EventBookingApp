using System;
using EventBooking.Application.Common.Models.Users;

namespace EventBooking.Application.UnitTest.TestUtils.Models;

public static class UserModelUtils
{
    public static UserModel CreateUserModel() => new UserModel(1, "name", "username", "email", CreateUserAddress(), "0123456", "website", CreateCompany());
    private static UserAddress CreateUserAddress() => new UserAddress("street", "suite", "city", "zipcode", CreateGeo());
    private static Company CreateCompany() => new Company("name", "catchPhrase", "Bs");
    private static Geo CreateGeo() => new Geo("lat", "Lng");
}

