using System.Threading.Tasks;

namespace SmartSolution.SharedKernel.Domain.Seedwork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> AddAsync(T entity);

        Task<T> GetAsync(int id);

        T UpdateAsync(T entity);

        bool DeleteAsync(T entity);
    }
}
