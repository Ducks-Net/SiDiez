using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;

namespace DucksNet.Infrastructure.Sqlite;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
{

    private readonly IDatabaseContext _context;
    
    public RepositoryAsync(IDatabaseContext context)
    {
        this._context = context;
    }

    public async Task<Result<T>> GetAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return Result<T>.Error($"Entity of type {typeof(T).Name} was not found.");
        }

        return Result<T>.Ok(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<Result> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }
    
    public async Task<Result> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }
    
    public async Task<Result> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }
}
