using MediatR;
using SmartSolution.Domain.AggregatesModel.TaskAggregate;
using SmartSolution.Domain.Exceptions;
using SmartSolution.Identity.Auth;
using SmartSolution.Infrastructure.Commands;
using SmartSolution.Infrastructure.Idempotency;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Task.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
    {
        private readonly ICrmTaskRepository _crmTaskRepository;
        private readonly IUserManager _userManager;

        public CreateTaskCommandHandler(ICrmTaskRepository crmTaskRepository, IUserManager userManager)
        {
            _crmTaskRepository = crmTaskRepository;
            _userManager = userManager;
        }

        public async Task<bool> Handle(CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _userManager.GetCurrentUserId();

            var task = new CrmTask();
            task.AddToInfo(request.Title, request.Description, request.Deadline, 1);

            request.Employees.ForEach(p => task.AddEmployee(p));
            task.SetAuditFields(userId);

            await _crmTaskRepository.AddAsync(task);
            return await _crmTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreateTaskIdentifiedCommandHandler :
        IdentifiedCommandHandler<CreateTaskCommand, bool>
    {
        public CreateTaskIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests
        }
    }
}
