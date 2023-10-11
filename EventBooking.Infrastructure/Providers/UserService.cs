using System;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Services;
using EventBooking.Application.Common.Models.Users;
using EventBooking.Domain.Common;
using EventBooking.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace EventBooking.Infrastructure.Providers;

public class UserService : IUserService
{
    private readonly InfrastructureSetting _infrastructureSetting;
    public UserService(IOptions<InfrastructureSetting> infrastructureSetting)
    {
        _infrastructureSetting = infrastructureSetting.Value;
    }
    public async Task<ErrorOr<UserModel>> GetUserByIdAsync(int id)
    {
        var client = new RestClient(_infrastructureSetting.UserBaseUrl);
        var request = new RestRequest($"users/{id}", Method.Get);
        var response = await client.ExecuteAsync<UserModel>(request);
        if (!response.IsSuccessful)
        {
            return UserErrors.UserNotFound;
        }
        return response.Data;

    }
}


