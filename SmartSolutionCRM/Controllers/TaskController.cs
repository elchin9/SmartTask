using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.SharedKernel.Domain.Seedwork;
using SmartSolution.Task.Commands;
using SmartSolutionCRM.Extensions;
using SmartSolutionCRM.Utilities;
using System;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Controllers
{

    [Route("task")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            await _mediator.ExecuteIdentifiedCommand<CreateTaskCommand, bool>(command, requestId);
            return NoContent();
        }
    }
}
