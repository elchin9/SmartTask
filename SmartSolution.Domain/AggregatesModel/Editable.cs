using SmartSolution.Domain.AggregatesModel.UserAggregate;
using System;

namespace SmartSolution.Domain.AggregatesModel
{
    public class Editable<TUser> : Auditable<TUser> where TUser : User
    {
        public int? UpdatedById { get; protected set; }

        public DateTime? LastUpdateDateTime { get; protected set; }

        public TUser UpdatedBy { get; protected set; }

        public void SetAuditFields(int? updatedById, DateTime? lastUpdateDateTime)
        {
            UpdatedById = updatedById;
            LastUpdateDateTime = lastUpdateDateTime;
        }
    }
}
