using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;

public interface IRepository<T> where T : class
{
    Result<T> Get(Guid id);
    IEnumerable<T> GetAll();
    Result Add(T entity);
    Result Update(T entity);
    Result Delete(T entity);
}
