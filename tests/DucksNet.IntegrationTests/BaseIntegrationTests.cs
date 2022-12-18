using System;
using System.IO;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;

namespace DucksNet.IntegrationTests;

public class BaseIntegrationTests<T> : WebApplicationFactory<Program> where T : class
{
    protected HttpClient TestingClient { get; private set; }
    public BaseIntegrationTests()
    {
        ClearDatabase();
        // Use HTTPS by default and do not follow
        // redirects so they can tested explicitly.
        ClientOptions.AllowAutoRedirect = false;
        ClientOptions.BaseAddress = new Uri("https://localhost");

        TestingClient = CreateDefaultClient();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            // Configure the test fixture to write the SQLite database
            // to a temporary directory, rather than in App_Data.
            var dataDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        });
        
        // Configure the correct content root for the static content and Razor pages
        builder.UseSolutionRelativeContentRoot(Path.Combine("src", "DucksNet.API"));

        // Configure the application so HTTP requests related to the OAuth flow
        // can be intercepted and redirected to not use the real GitHub service.
        builder.ConfigureServices(services =>
        {
            services.AddHttpClient();
            services.AddScoped<IDatabaseContext, TestDbContext>(_ => new TestDbContext(typeof(T).FullName!));
        });
    }
    
    protected void ClearDatabase()
    {
        var context = new TestDbContext(typeof(T).FullName!);
        context.RemoveRange(context.Cages);
        context.RemoveRange(context.CageTimeBlocks);
        context.RemoveRange(context.Appointments);
        context.RemoveRange(context.Pets);
        context.RemoveRange(context.Users);
        context.RemoveRange(context.MedicalRecords);
        context.RemoveRange(context.Employees);
        context.RemoveRange(context.Treatments);
        context.RemoveRange(context.Medicines);
        context.RemoveRange(context.Offices);
        context.RemoveRange(context.Businesses);
        context.SaveChanges();
        context.Dispose();
    }
}
