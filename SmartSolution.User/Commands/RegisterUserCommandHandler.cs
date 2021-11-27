using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Domain.Exceptions;
using SmartSolution.Identity.Auth;
using SmartSolution.Identity.Queries;
using SmartSolution.Infrastructure.Commands;
using SmartSolution.Infrastructure.Database;
using SmartSolution.Infrastructure.Idempotency;
using SmartSolution.SharedKernel.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.User.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _userQueries;
        private readonly SmartSolutionDbContext _context;
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUserManager userManager, IUserQueries userQueries, SmartSolutionDbContext context)
        {
            _userRepository = userRepository;
            _userQueries = userQueries;
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.GetCurrentUser();
            var userName = request.Email.ToLower();
            var existingUser = await _userQueries.FindByNameAsync(userName);

            if (existingUser != null)
            {
                throw new DomainException($"Email '{request.Email}' already taken, please choose another email.");
            }

            var user = new Domain.AggregatesModel.UserAggregate.User(userName, request.Email, PasswordHasher.HashPassword(userName, request.Password),request.FirstName, request.LastName, currentUser.OrganizationId);

            var role = await _context.Roles.FirstOrDefaultAsync(p => p.Name == RoleParametr.User.Name);
            user.AddToRole(role.Id);

            await _userRepository.AddAsync(user);
            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class RegisterUserIdentifiedCommandHandler :
        IdentifiedCommandHandler<RegisterUserCommand, bool>
    {
        public RegisterUserIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests
        }
    }
}
