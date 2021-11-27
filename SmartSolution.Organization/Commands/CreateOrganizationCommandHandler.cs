using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Domain.Exceptions;
using SmartSolution.Identity.Queries;
using SmartSolution.Infrastructure.Commands;
using SmartSolution.Infrastructure.Database;
using SmartSolution.Infrastructure.Idempotency;
using SmartSolution.SharedKernel.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Organization.Commands
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _userQueries;
        private readonly SmartSolutionDbContext _context;

        public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, SmartSolutionDbContext context, IUserRepository userRepository, IUserQueries userQueries)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _userQueries = userQueries;
            _context = context;
        }

        public async Task<bool> Handle(CreateOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var organization = new Domain.AggregatesModel.OrganizationAggregate.Organization();
            organization.AddToInfo(request.Name, request.PhoneNumber, request.Address);

            await _organizationRepository.AddAsync(organization);
            await _organizationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var userName = request.Email.ToLower();
            var existingUser = await _userQueries.FindByEmailAsync(userName);

            if (existingUser != null)
            {
                throw new DomainException($"Email '{request.Email}' already taken, please choose another email.");
            }

            var user = new Domain.AggregatesModel.UserAggregate.User(userName, request.Email, PasswordHasher.HashPassword(userName, request.Password), request.FirstName, request.LastName, organization.Id);

            var role = await _context.Roles.FirstOrDefaultAsync(p => p.Name == RoleParametr.Admin.Name);
            user.AddToRole(role.Id);

            await _userRepository.AddAsync(user);
            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreateOrganizationIdentifiedCommandHandler :
        IdentifiedCommandHandler<CreateOrganizationCommand, bool>
    {
        public CreateOrganizationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests
        }
    }
}