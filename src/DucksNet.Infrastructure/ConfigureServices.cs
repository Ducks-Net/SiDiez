using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.SqliteAsync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DucksNet.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddScoped<IRepositoryAsync<Cage>, RepositoryAsync<Cage>>();
        services.AddScoped<IRepositoryAsync<CageTimeBlock>, RepositoryAsync<CageTimeBlock>>();
        services.AddScoped<IRepositoryAsync<Appointment>, RepositoryAsync<Appointment>>();
        services.AddScoped<IRepositoryAsync<Pet>, RepositoryAsync<Pet>>();
        services.AddScoped<IRepositoryAsync<User>, RepositoryAsync<User>>();
        services.AddScoped<IRepositoryAsync<Treatment>, RepositoryAsync<Treatment>>();
        services.AddScoped<IRepositoryAsync<Medicine>, RepositoryAsync<Medicine>>();
        services.AddScoped<IRepositoryAsync<MedicalRecord>, RepositoryAsync<MedicalRecord>>();
        services.AddScoped<IRepositoryAsync<Employee>, RepositoryAsync<Employee>>();
        services.AddScoped<IRepositoryAsync<Business>, RepositoryAsync<Business>>();
        services.AddScoped<IRepositoryAsync<Office>, RepositoryAsync<Office>>();
        services.AddScoped<IDatabaseContext, DatabaseContext>();
        services.AddDbContext<DatabaseContext>();


        return services;
    }
}

