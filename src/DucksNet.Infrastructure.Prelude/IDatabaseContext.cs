using Microsoft.EntityFrameworkCore;

using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Prelude;

public interface IDatabaseContext
{
    DbSet<T> Set<T>() where T : class;

    DbSet<Cage> Cages { get; }
    DbSet<CageTimeBlock> CageTimeBlocks { get; }
    DbSet<Business> Businesses { get; }
    DbSet<Office> Offices { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<Pet> Pets { get; }
    DbSet<User> Users { get; }
    DbSet<MedicalRecord> MedicalRecords { get; }
    DbSet<Treatment> Treatments { get; }
    DbSet<Medicine> Medicines { get; }
    DbSet<Employee> Employees { get; }

    Task SaveChangesAsync();
}
