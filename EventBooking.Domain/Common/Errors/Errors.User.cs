using System;
using ErrorOr;

namespace EventBooking.Domain.Common
{
    public static class UserErrors
    {
        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found");
    }
}

