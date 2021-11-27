using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Identity.Queries;
using SmartSolution.Identity.ViewModels;
using SmartSolution.SharedKernel.Domain.Seedwork;
using SmartSolution.User.Commands;
using SmartSolutionCRM.Extensions;
using SmartSolutionCRM.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            await _mediator.ExecuteIdentifiedCommand<RegisterUserCommand, bool>(command, requestId);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _userQueries.GetAllUsers();
        }
    }
}
