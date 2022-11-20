using DucksNet.Infrastructure.Sqlite;

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace DucksNet.API.Integration_Tests;

public class BaseIntegrationTests<T> where T : class
{
    protected HttpClient HttpClient { get; private set; }
    protected BaseIntegrationTests()
    {
        var application = new WebApplicationFactory<T>().WithWebHostBuilder(builder => { });
        HttpClient = application.CreateClient();
        CleanDatabases();
    }

    private void CleanDatabases()
    {
        DatabaseContext databaseContext = new DatabaseContext();
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
