using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace SamuraiApp.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext()
    {
        this.Database.EnsureCreated(); //just for integration tests
    }
    public DbSet<Cage> Cages => Set<Cage>();

    public DbSet<CageTimeBlock> CageTimeBlocks => Set<CageTimeBlock>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = DucksNet.db");
    }
    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
