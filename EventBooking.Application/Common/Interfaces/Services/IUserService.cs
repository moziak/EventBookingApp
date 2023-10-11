using System;
using ErrorOr;
using EventBooking.Application.Common.Models.Users;

namespace EventBooking.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<ErrorOr<UserModel>> GetUserByIdAsync(int id);
}


