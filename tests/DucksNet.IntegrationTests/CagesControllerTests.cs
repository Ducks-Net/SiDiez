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

    [Fact]
    public void When_Get_WithValidId_ShouldReturnCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = TestingClient.GetAsync($"{CagesURI}/{cageId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = cageResponse.Content.ReadFromJsonAsync<Cage>().Result;
        cage.Should().NotBeNull();
        cage!.ID.Should().Be(cageId);
        cage!.LocationId.Should().Be(officeId);
        cage!.Size.Should().Be(Size.Small);
    }

    [Fact]
    public void When_Get_WithInvalidId_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = TestingClient.GetAsync($"{CagesURI}/{Guid.NewGuid()}").Result;

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void When_GetByLocation_WithValidLocation_ShouldReturnCages()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = TestingClient.GetAsync($"{CagesURI}/byLocation/{officeId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = cageResponse.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().NotBeEmpty();
        cages.Should().Contain(c => c.ID == cageId);
    }

    [Fact]
    public void When_GetByLocation_SholdReturnOnlyFromThatLocation()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        var officeId2 = SetupOffice();
        var cageId2 = SetupCage(officeId2, Size.Small.Name);
        
        //Act
        var cageResponse = TestingClient.GetAsync($"{CagesURI}/byLocation/{officeId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = cageResponse.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().NotBeEmpty();
        cages.Should().Contain(c => c.ID == cageId);
        cages.Should().NotContain(c => c.ID == cageId2);
    }

    [Fact]
    public void When_GetByLocation_WithInvalidLocation_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = TestingClient.GetAsync($"{CagesURI}/byLocation/{Guid.NewGuid()}").Result;

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void When_GetAll_Should_ReturnAllValues()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        var cageId2 = SetupCage(officeId, Size.Medium.Name);
        
        //Act
        var cageResponse = TestingClient.GetAsync(CagesURI).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = cageResponse.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().NotBeEmpty();
        cages.Should().HaveCount(2);
        cages.Should().Contain(c => c.ID == cageId);
        cages.Should().Contain(c => c.ID == cageId2);
    }

    [Fact]
    public void When_GetAll_Should_ReturnEmptyList()
    {
        ClearDatabase();
        //Arrange
        
        //Act
        var cageResponse = TestingClient.GetAsync(CagesURI).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = cageResponse.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().BeEmpty();
    }

    [Fact]
    public void When_Delete_WithValidId_ShouldDeleteCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = TestingClient.DeleteAsync($"{CagesURI}/{cageId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = TestingClient.GetAsync(CagesURI).Result.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().BeEmpty();
    }

    [Fact]
    public void When_Delete_WithInvalidId_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = TestingClient.DeleteAsync($"{CagesURI}/{Guid.NewGuid()}").Result;

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void When_Delete_ShouldOnlyDeleteSelected()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        var cageId2 = SetupCage(officeId, Size.Medium.Name);
        
        //Act
        var cageResponse = TestingClient.DeleteAsync($"{CagesURI}/{cageId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = TestingClient.GetAsync(CagesURI).Result.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().NotBeEmpty();
        cages.Should().HaveCount(1);
        cages.Should().Contain(c => c.ID == cageId2);
        cages.Should().NotContain(c => c.ID == cageId);
    }

    private Guid SetupCage(Guid officeId, string size)
    {        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = size
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesURI, sut).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = cageResponse.Content.ReadFromJsonAsync<Cage>().Result;
        return cage!.ID;
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
