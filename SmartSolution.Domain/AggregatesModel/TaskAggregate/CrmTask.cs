using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;

namespace SmartSolution.Domain.AggregatesModel.TaskAggregate
{
    public class CrmTask : Editable<User>, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Deadline { get; private set; }
        public int StatusId { get; private set; }


        public readonly List<TaskEmployees> _employees;
        public IReadOnlyCollection<TaskEmployees> Employees => _employees;

        public CrmTask()
        {
            _employees = new List<TaskEmployees>();
        }

        public void AddToInfo(string title, string description, DateTime deadline, int statusId)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
            StatusId = statusId;
        }

        public void AddEmployee(int userId)
        {
            _employees.Add(new TaskEmployees(userId));
        }
    }
}
