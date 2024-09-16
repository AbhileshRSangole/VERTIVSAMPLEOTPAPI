using OtpApi.Models;
using System;
using System.Collections.Concurrent;

namespace OtpApi.Services
{
    public class OtpService : IOtpService
    {
        private readonly ConcurrentDictionary<string, Otp> _otps = new ConcurrentDictionary<string, Otp>();
        private readonly TimeSpan _validityPeriod = TimeSpan.FromSeconds(60);


        public Otp GenerateOtp(string userId, DateTime dateTime)
        {
            var password = Guid.NewGuid().ToString().Substring(0, 4);
            var otp = new Otp
            {
                UserId = userId,
                Password = password,
                GeneratedTime = dateTime
            };
            _otps[userId] = otp;
            return otp;
        }

        public bool ValidateOtp(string userId, string password, out TimeSpan remainingTime)
        {
            remainingTime = TimeSpan.Zero;

            if (_otps.TryGetValue(userId, out var otp))
            {
                var elapsed = DateTime.UtcNow - otp.GeneratedTime;
                if (elapsed < _validityPeriod && otp.Password == password)
                {
                    remainingTime = _validityPeriod - elapsed;
                    return true;
                }
            }
            return false;
        }
    }
}
