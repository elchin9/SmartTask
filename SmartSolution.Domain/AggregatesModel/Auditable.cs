using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Domain.Exceptions;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System;

namespace SmartSolution.Domain.AggregatesModel
{
    public class Auditable<TUser> : Entity where TUser : User
    {
        public int CreatedById { get; protected set; }

        public DateTime RecordDateTime { get; protected set; }

        public TUser CreatedBy { get; protected set; }

        public void SetAuditFields(int createdById)
        {
            if (CreatedById != 0 && CreatedById != createdById)
            {
                throw new DomainException("CreatedBy already set");
            }

            CreatedById = createdById;
            RecordDateTime = DateTime.UtcNow.AddHours(4);
        }
    }
}
