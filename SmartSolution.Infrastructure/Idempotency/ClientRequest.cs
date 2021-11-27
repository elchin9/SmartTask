using System;

namespace SmartSolution.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public int Id { get; set; }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
    }
}
