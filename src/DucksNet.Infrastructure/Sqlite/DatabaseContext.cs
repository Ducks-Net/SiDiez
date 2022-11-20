using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    DbSet<Cage> IDatabaseContext.Cages => Set<Cage>();
    DbSet<CageTimeBlock> IDatabaseContext.CageTimeBlocks => Set<CageTimeBlock>();

    DbSet<Treatment> IDatabaseContext.Treatments => Set<Treatment>();
    DbSet<Medicine> IDatabaseContext.Medicines => Set<Medicine>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = DucksNet.db");
    }
    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
