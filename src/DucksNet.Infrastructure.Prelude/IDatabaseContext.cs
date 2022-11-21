using Microsoft.EntityFrameworkCore;

using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Prelude;

public interface IDatabaseContext
{
    DbSet<Cage> Cages { get; }
    DbSet<CageTimeBlock> CageTimeBlocks { get; }
    DbSet<Business> Businesses { get; }
    DbSet<Office> Offices { get; }
    void SaveChanges();
}
