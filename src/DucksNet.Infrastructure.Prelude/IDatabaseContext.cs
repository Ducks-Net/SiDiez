using Microsoft.EntityFrameworkCore;

using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Prelude;

public interface IDatabaseContext
{
    DbSet<Cage> Cages { get; }
    DbSet<CageTimeBlock> CageTimeBlocks { get; }

    DbSet<Treatment> Treatments { get; }

    DbSet<Medicine> Medicines { get; }

    void SaveChanges();
}
