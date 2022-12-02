using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class UsersRepository : IRepository<User>
{
    private readonly IDatabaseContext _databaseContext;

    public UsersRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<User> IRepository<User>.Get(Guid id)
    {
        var user = _databaseContext.Users.Find(id);
        return user is null ? Result<User>.Error("User not found") : Result<User>.Ok(user);
    }

    IEnumerable<User> IRepository<User>.GetAll()
    {
        return _databaseContext.Users;
    }

    Result IRepository<User>.Add(User entity)
    {
        _databaseContext.Users.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<User>.Update(User entity)
    {
        _databaseContext.Users.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<User>.Delete(User entity)
    {
        _databaseContext.Users.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
