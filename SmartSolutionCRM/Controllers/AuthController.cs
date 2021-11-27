using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSolution.Identity.Auth;
using SmartSolution.Identity.Queries;
using SmartSolution.Identity.ViewModels;
using SmartSolution.User.Commands;
using System;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserManager _userManager;
        private readonly IUserQueries _userQueries;

        public AuthController(IMediator mediator, IUserManager userManager, IUserQueries userQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _userQueries = userQueries ?? throw new ArgumentException(nameof(userQueries));
        }

        [HttpGet("profile")]
        public async Task<UserProfileDto> Profile()
        {
            var profile = await _userQueries.GetUserProfileAsync(_userManager.GetCurrentUserId());

            return profile;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] GetAuthorizationTokenCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshUserTokenCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}
