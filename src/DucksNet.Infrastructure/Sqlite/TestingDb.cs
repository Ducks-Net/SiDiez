using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.EntityFrameworkCore;
namespace DucksNet.Infrastructure.Sqlite;

public class TestingDb : DbContext, IDatabaseContext
{
    public string TestName { get; }
    public TestingDb(string testName)
    {
        // NOTE (dvx): just for integration tests
        // TODO (AL): make a special context and separate db for integration tests
        this.Database.EnsureCreated();
        this.TestName = testName; 
    }

    ~TestingDb()
    {
    }

    public DbSet<Cage> Cages => Set<Cage>();
    public DbSet<CageTimeBlock> CageTimeBlocks => Set<CageTimeBlock>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<User> Users => Set<User>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<Business> Businesses => Set<Business>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source = DucksNetTesting{TestName}.db");
    }
    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
