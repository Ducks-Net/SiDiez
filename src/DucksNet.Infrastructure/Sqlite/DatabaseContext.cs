using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.Infrastructure.Prelude;

using Microsoft.EntityFrameworkCore;
namespace DucksNet.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext()
    {
        // NOTE (Al): Make sure the database is created. 
        this.Database.EnsureCreated(); 
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
        optionsBuilder.UseSqlite("Data Source = DucksNet.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cage>()
            .Property(c => c.Size)
            .HasConversion(
                v => v.ToString(),
                v => Size.CreateFromString(v).Value!);
        
        modelBuilder.Entity<Pet>()
            .Property(p => p.Size)
            .HasConversion(
                v => v.ToString(),
                v => Size.CreateFromString(v).Value!);
    }

    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
