
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;


namespace DucksNet.IntegrationTests;

public class BaseIntegrationTests<T> where T : class
{
    protected HttpClient TestingClient { get; private set; }
    protected WebApplicationFactory<T> TestingFactory { get; private set; }
    protected BaseIntegrationTests()
    {
        ClearDatabase();
        TestingFactory = new WebApplicationFactory<T>().WithWebHostBuilder(builder => {
            builder.ConfigureTestServices(services => {
                services.AddScoped<IDatabaseContext>(provider => new TestDbContext(typeof(T).FullName!));
            });
         });
        TestingClient = TestingFactory.CreateClient();
    }
    
    private void ClearDatabase()
    {
        var context = new TestDbContext(typeof(T).FullName!);
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
