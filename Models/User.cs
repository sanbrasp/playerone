using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerOne.Models
{
    internal class User
    {
        public int UserId { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
