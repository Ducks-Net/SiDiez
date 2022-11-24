﻿using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using static System.Guid;

namespace DucksNet.API.Integration_Tests;
public class MedicalRecordsControllerTests
{
    private const string ApiURL = "api/medicalrecords";
    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        await using var application = new WebApplicationFactory<DucksNet.API.Controllers.MedicalRecordsController>().WithWebHostBuilder(builder => { 
            builder.ConfigureTestServices(services => {
                services.AddScoped<IDatabaseContext>(provider => new TestDbContext("MedicalRecordsControllerTests"));
            });
        });
        using var client = application.CreateClient();

        //Arrange
        //should get the an appointment and client using get and their id
        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO
        {
            IdAppointment = System.Guid.NewGuid(),
            IdClient = System.Guid.NewGuid()
        };

        //Act
        var createShelterResponse = await client.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var getShelterResult = await client.GetAsync(ApiURL);

        //Assert
        createShelterResponse.EnsureSuccessStatusCode();
        createShelterResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }
}
