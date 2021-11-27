using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSolution.Organization.Commands;
using SmartSolutionCRM.Extensions;
using System;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Controllers
{
    [Route("organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrganizationCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            await _mediator.ExecuteIdentifiedCommand<CreateOrganizationCommand, bool>(command, requestId);
            return NoContent();
        }
    }
}
