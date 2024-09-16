using System;

namespace OtpApi.Models
{
    public class Otp
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime GeneratedTime { get; set; }
    }
}
