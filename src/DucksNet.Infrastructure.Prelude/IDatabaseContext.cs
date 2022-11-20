using Microsoft.EntityFrameworkCore;

using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Prelude;

public interface IDatabaseContext
{
    DbSet<Cage> Cages { get; }
    DbSet<CageTimeBlock> CageTimeBlocks { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<Pet> Pets { get; }
    DbSet<User> Users { get; }

    void SaveChanges();
}
