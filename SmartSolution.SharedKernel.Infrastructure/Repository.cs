using Microsoft.EntityFrameworkCore;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System.Threading.Tasks;

namespace SmartSolution.SharedKernel.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        public abstract DbContext Context { get; protected set; }

        public IUnitOfWork UnitOfWork => Context as IUnitOfWork;

        public async Task<T> AddAsync(T entity)
        {
            var entry = await Context.Set<T>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public T UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public bool DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);

            return true;
        }
    }
}
