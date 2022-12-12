using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;

public interface IRepositoryAsync<T> where T : class
{
    Task<Result<T>> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<Result> AddAsync(T entity);
    Task<Result> UpdateAsync(T entity);
    Task<Result> DeleteAsync(T entity);
}
