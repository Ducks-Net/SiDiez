using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext()
    {
        // NOTE (dvx): just for integration tests
        // TODO (AL): make a special context and separate db for integration tests
        this.Database.EnsureCreated(); 
    }

    DbSet<Cage> IDatabaseContext.Cages => Set<Cage>();
    DbSet<CageTimeBlock> IDatabaseContext.CageTimeBlocks => Set<CageTimeBlock>();
    DbSet<Appointment> IDatabaseContext.Appointments => Set<Appointment>();
    DbSet<Pet> IDatabaseContext.Pets => Set<Pet>();
    DbSet<User> IDatabaseContext.Users => Set<User>();
    DbSet<MedicalRecord> IDatabaseContext.MedicalRecords => Set<MedicalRecord>();
    DbSet<Employee> IDatabaseContext.Employees => Set<Employee>();
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
