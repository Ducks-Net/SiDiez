using Microsoft.AspNetCore.Mvc.Testing;
using DucksNet.Infrastructure.Sqlite;

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
        var databaseContext = new DatabaseContext();
        databaseContext.Cages.RemoveRange(databaseContext.Cages.ToList());
        databaseContext.CageTimeBlocks.RemoveRange(databaseContext.CageTimeBlocks.ToList());
        databaseContext.MedicalRecords.RemoveRange(databaseContext.MedicalRecords.ToList());
        databaseContext.SaveChanges();
    }
}
