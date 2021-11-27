using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.Exceptions;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System.Collections.Generic;

namespace SmartSolution.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public int OrganizationId { get; private set; }
        public int RoleId { get; private set; }

        public string RefreshToken { get; set; }

        public Organization Organization { get; private set; }
        public Role Role { get; private set; }




        protected User()
        {
        }

        public User(string userName, string email, string passwordHash, string firstName, string lastName, int organizationId) : this()
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            OrganizationId = organizationId;
        }

        public void AddToRole(int roleId)
        {
            RoleId = roleId;
        }


        public void SetDetails(string userName, string email, string passwordHash, string firstName, string lastName, int organizationId)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            OrganizationId = organizationId;
        }


        public void SetPasswordHash(string oldPasswordHash, string newPasswordHash)
        {
            if (PasswordHash != oldPasswordHash)
            {
                throw new DomainException("Invalid old password");
            }

            if (PasswordHash != newPasswordHash)
            {
                PasswordHash = newPasswordHash;
            }
        }

        public void ResetPassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}
