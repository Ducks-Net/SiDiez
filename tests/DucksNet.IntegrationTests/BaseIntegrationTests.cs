using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DucksNet.API.Integration_Tests;

public class BaseIntegrationTests<T> where T : class
{
    protected HttpClient HttpClient { get; private set; }
    protected BaseIntegrationTests()
    {
        var application = new WebApplicationFactory<T>().WithWebHostBuilder(builder => 
        {
            builder.ConfigureTestServices(services =>
            {
                // var serviceProvider = services.BuildServiceProvider();
                // var descriptor =
                //     new ServiceDescriptor(
                //         typeof(IDatabaseContext),
                //         typeof(TestingDb),
                //         ServiceLifetime.Transient);
                services.AddDbContext<IDatabaseContext, TestingDb>();
                // services.Replace(descriptor);
            });
        });
        HttpClient = application.CreateDefaultClient();
        CleanDatabases();
    }

    private void CleanDatabases()
    {
        TestingDb databaseContext = new TestingDb(nameof(T));
        databaseContext.Cages.RemoveRange(databaseContext.Cages);
        databaseContext.CageTimeBlocks.RemoveRange(databaseContext.CageTimeBlocks);
        databaseContext.MedicalRecords.RemoveRange(databaseContext.MedicalRecords);
        databaseContext.Medicines.RemoveRange(databaseContext.Medicines);
        databaseContext.Treatments.RemoveRange(databaseContext.Treatments);
        databaseContext.Employees.RemoveRange(databaseContext.Employees);
        databaseContext.Appointments.RemoveRange(databaseContext.Appointments);
        databaseContext.Pets.RemoveRange(databaseContext.Pets);
        databaseContext.Users.RemoveRange(databaseContext.Users);
        databaseContext.SaveChanges();
    }
}
