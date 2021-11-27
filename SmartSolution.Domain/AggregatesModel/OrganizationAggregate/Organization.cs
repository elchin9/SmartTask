using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System.Collections.Generic;

namespace SmartSolution.Domain.AggregatesModel.OrganizationAggregate
{
    public class Organization : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Address { get; private set; }


        private readonly List<User> _users;

        public IReadOnlyCollection<User> Users => _users;


        public Organization()
        {
            _users = new List<User>();
        }

        public void AddToInfo(string name, string phoneNumber, string address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
