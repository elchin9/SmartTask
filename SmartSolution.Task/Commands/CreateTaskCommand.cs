using MediatR;
using System;
using System.Collections.Generic;

namespace SmartSolution.Task.Commands
{
    public class CreateTaskCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public List<int> Employees { get; set; }
    }
}
