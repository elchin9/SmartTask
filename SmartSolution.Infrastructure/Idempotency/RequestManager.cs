using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.Exceptions;
using SmartSolution.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace SmartSolution.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly SmartSolutionDbContext _context;

        public RequestManager(SmartSolutionDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExistAsync(Guid key)
        {
            var request = await _context.Set<ClientRequest>().SingleOrDefaultAsync(r => r.Key == key);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid key)
        {
            var exists = await ExistAsync(key);

            var request = exists
                ? throw new DomainException($"Request with key {key} already exists.")
                : new ClientRequest
                {
                    Key = key,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}
