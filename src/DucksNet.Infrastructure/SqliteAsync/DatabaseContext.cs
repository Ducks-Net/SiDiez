using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.Infrastructure.Prelude;

using Microsoft.EntityFrameworkCore;
namespace DucksNet.Infrastructure.SqliteAsync;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        // NOTE (Al): Make sure the database is created. 
        Database.EnsureCreated();
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
                value => value.Id,
                id => Size.CreateFromInt(id).Value!);

        modelBuilder.Entity<Pet>()
            .Property(p => p.Size)
            .HasConversion(
                value => value.Id,
                id => Size.CreateFromInt(id).Value!);

        modelBuilder.Entity<Appointment>()
            .Property(a => a.Type)
            .HasConversion(
                value => value.Id,
                id => AppointmentType.CreateFromInt(id).Value!);
        modelBuilder.Entity<Medicine>()
            .Property(a => a.DrugAdministration)
            .HasConversion(
                value => value.Id,
                id => DrugAdministration.createMedicineByInt(id).Value!);
    }

    Task IDatabaseContext.SaveChangesAsync()
    {
        return SaveChangesAsync();
    }
}
