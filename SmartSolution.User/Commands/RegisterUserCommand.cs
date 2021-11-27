using MediatR;
using System;

namespace SmartSolution.User.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

