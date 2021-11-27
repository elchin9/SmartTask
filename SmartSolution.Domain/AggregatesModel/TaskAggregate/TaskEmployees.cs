using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.SharedKernel.Domain.Seedwork;

namespace SmartSolution.Domain.AggregatesModel.TaskAggregate
{
    public class TaskEmployees : Editable<User>, IAggregateRoot
    {
        public int UserId { get; set; }
        public int CrmTaskId { get; set; }

        public CrmTask CrmTask { get; set; }
        public User User { get; set; }

        public TaskEmployees(int userId)
        {
            UserId = userId;
        }
    }
}
