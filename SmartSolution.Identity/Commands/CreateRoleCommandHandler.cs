using MediatR;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Identity.Auth;
using SmartSolution.Infrastructure.Commands;
using SmartSolution.Infrastructure.Idempotency;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Identity.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserManager _userManager;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IUserManager userManager)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Name);

            await _roleRepository.AddAsync(role);
            await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }

        public class CreateRoleIdentifiedCommandHandler : IdentifiedCommandHandler<CreateRoleCommand, bool>
        {
            public CreateRoleIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true; // Ignore duplicate requests
            }
        }
    }
}
