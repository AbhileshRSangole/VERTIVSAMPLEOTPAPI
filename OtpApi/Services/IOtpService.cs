using OtpApi.Models;
using System;

namespace OtpApi.Services
{
    public interface IOtpService
    {
        Otp GenerateOtp(string userId, DateTime dateTime);
        bool ValidateOtp(string userId, string password, out TimeSpan remainingTime);
    }
}
