using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext()
    {
        this.Database.EnsureCreated(); //just for integration tests
    }
    public DbSet<Cage> Cages => Set<Cage>();

    public DbSet<CageTimeBlock> CageTimeBlocks => Set<CageTimeBlock>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Employee> Employees => Set<Employee>();

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
