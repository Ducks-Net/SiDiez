using Microsoft.EntityFrameworkCore;

using DucksNet.Infrastructure.Prelude;
using DucksNet.Domain.Model;

namespace DucksNet.Infrastructure.Sqlite;

public class DatabaseContext : DbContext, IDatabaseContext
{
    DbSet<Cage> IDatabaseContext.Cages => Set<Cage>();
    DbSet<CageTimeBlock> IDatabaseContext.CageTimeBlocks => Set<CageTimeBlock>();
    DbSet<Appointment> IDatabaseContext.Appointments => Set<Appointment>();
    DbSet<Pet> IDatabaseContext.Pets => Set<Pet>();
    DbSet<User> IDatabaseContext.Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = DucksNet.db");
    }
    void IDatabaseContext.SaveChanges()
    {
        SaveChanges();
    }
}
