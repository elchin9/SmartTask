using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System.Collections.Generic;

namespace SmartSolution.Domain.AggregatesModel.RoleAggregate
{
    public class Role : Entity, IAggregateRoot
    {
        public string Name { get; private set; }


        private readonly List<User> _users;
        public IReadOnlyCollection<User> Users => _users;


        protected Role()
        {
            _users = new List<User>();
        }

        public Role(string name) : this()
        {
            Name = name;
        }

        public void SetDetails(string name)
        {
            Name = name;
        }
    }
}
