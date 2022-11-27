using System;
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
        //Arrange
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
        
        var sut = new CreateCageDTO {
            LocationId = office!.ID,
            SizeString = "Small"
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesURI, sut).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = cageResponse.Content.ReadFromJsonAsync<Cage>().Result;
        cage.Should().NotBeNull();
        cage!.LocationId.Should().Be(office.ID);
        cage!.Size.Should().Be(Size.Small);
    }
}
