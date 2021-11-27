using MediatR;

namespace SmartSolution.Identity.Commands
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}

