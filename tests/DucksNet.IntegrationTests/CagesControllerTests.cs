using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace  DucksNet.IntegrationTests;

public class CagesControllerTests : BaseIntegrationTests<CagesController>
{
    private const string CagesURI = "api/v1/cages";
    private const string OfficesUrl = "api/v1/office";

    [Fact]
    public void When_Post_WithValidData_ShouldReturnCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = "Small"
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesURI, sut).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = cageResponse.Content.ReadFromJsonAsync<Cage>().Result;
        cage.Should().NotBeNull();
        cage!.LocationId.Should().Be(officeId);
        cage!.Size.Should().Be(Size.Small);
    }

    [Fact]
    public void When_Post_WithBadLocation_ShouldReturnBadRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = new CreateCageDTO {
            LocationId = Guid.NewGuid(),
            SizeString = "Small"
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesURI, sut).Result;

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = cageResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Office not found.");
    }

    [Fact]
    public void When_Post_WithBadSize_ShouldReturnBadRequest()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = "Invalid"
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesURI, sut).Result;

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = cageResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Failed to parse cage size.");
    }

    private Guid SetupOffice()
    {
        var officeDTO = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var officeResponse = TestingClient.PostAsJsonAsync(OfficesUrl, officeDTO).Result;
        officeResponse.EnsureSuccessStatusCode();
        var office = officeResponse.Content.ReadFromJsonAsync<Office>().Result;
        office.Should().NotBeNull();
        return office!.ID;
    }
}
