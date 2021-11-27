using System;

namespace SmartSolution.User.Commands.Models
{
    public class JwtTokenDto
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}
