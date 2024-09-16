using NUnit.Framework;
using OtpApi.Models;
using OtpApi.Services;
using System;

namespace OtpApi.Tests
{
    public class OtpServiceTests
    {
        private IOtpService _otpService;

        [SetUp]
        public void Setup()
        {
            _otpService = new OtpService();
        }

        [Test]
        public void GenerateOtp()
        {
            var userId = "dummy value";
            var otp = _otpService.GenerateOtp(userId, DateTime.UtcNow);

            Assert.That(otp, Is.Not.Null, "Generated OTP should not be null");
            Assert.That(otp.UserId, Is.EqualTo(userId), "User ID in OTP does not match");
            Assert.That(otp.Password, Is.Not.Null, "OTP Password should not be null");
        }

        [Test]
        public void ValidateOtp()
        {
            var userId = "dummy value";
            var otp = _otpService.GenerateOtp(userId, DateTime.UtcNow);

            System.Threading.Thread.Sleep(1000);

            var isValid = _otpService.ValidateOtp(userId, otp.Password, out var remainingTime);

            Assert.That(isValid, Is.True, "OTP should be valid");
            Assert.That(remainingTime.TotalSeconds, Is.LessThanOrEqualTo(60), "Remaining time should be within 60 seconds");
        }
    }
}
