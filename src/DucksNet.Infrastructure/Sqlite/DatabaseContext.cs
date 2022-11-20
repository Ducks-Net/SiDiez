using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace SamuraiApp.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    DbSet<Cage> IDatabaseContext.Cages => Set<Cage>();
    DbSet<CageTimeBlock> IDatabaseContext.CageTimeBlocks => Set<CageTimeBlock>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = DucksNet.db");
    }
    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
