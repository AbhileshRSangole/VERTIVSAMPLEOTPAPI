using Microsoft.AspNetCore.Mvc;
using OtpApi.Models;
using OtpApi.Services;
using System;

namespace OtpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("generation")]
        public ActionResult<Otp> GenerateOtp([FromBody] string userId)
        {
            var otp = _otpService.GenerateOtp(userId, DateTime.UtcNow);
            return Ok(otp);
        }

        [HttpGet("validation")]
        public ActionResult ValidateOtp([FromQuery] string userId, [FromQuery] string password)
        {
            if (_otpService.ValidateOtp(userId, password, out var remainingTime))
            {
                return Ok(new { valid = true, remainingTime = remainingTime.TotalSeconds });
            }
            else
            {
                return Unauthorized(new { valid = false });
            }
        }
    }
}
