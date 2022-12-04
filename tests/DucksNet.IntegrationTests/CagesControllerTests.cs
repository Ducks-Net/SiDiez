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
    private const string CagesUrl = "api/v1/cages";
    private const string OfficesUrl = "api/v1/office";
    private const string PetsUrl = "api/v1/pets";

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
        var cageResponse = TestingClient.PostAsJsonAsync(CagesUrl, sut).Result;

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
        var cageResponse = TestingClient.PostAsJsonAsync(CagesUrl, sut).Result;

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
        var cageResponse = TestingClient.PostAsJsonAsync(CagesUrl, sut).Result;

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
        var cageResponse = TestingClient.GetAsync($"{CagesUrl}/{cageId}").Result;

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
        var cageResponse = TestingClient.GetAsync($"{CagesUrl}/{Guid.NewGuid()}").Result;

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
        var cageResponse = TestingClient.GetAsync($"{CagesUrl}/byLocation/{officeId}").Result;

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
        var cageResponse = TestingClient.GetAsync($"{CagesUrl}/byLocation/{officeId}").Result;

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
        var cageResponse = TestingClient.GetAsync($"{CagesUrl}/byLocation/{Guid.NewGuid()}").Result;

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
        var cageResponse = TestingClient.GetAsync(CagesUrl).Result;

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
        var cageResponse = TestingClient.GetAsync(CagesUrl).Result;

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
        var cageResponse = TestingClient.DeleteAsync($"{CagesUrl}/{cageId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = TestingClient.GetAsync(CagesUrl).Result.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().BeEmpty();
    }

    [Fact]
    public void When_Delete_WithInvalidId_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = TestingClient.DeleteAsync($"{CagesUrl}/{Guid.NewGuid()}").Result;

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
        var cageResponse = TestingClient.DeleteAsync($"{CagesUrl}/{cageId}").Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = TestingClient.GetAsync(CagesUrl).Result.Content.ReadFromJsonAsync<List<Cage>>().Result;
        cages.Should().NotBeEmpty();
        cages.Should().HaveCount(1);
        cages.Should().Contain(c => c.ID == cageId2);
        cages.Should().NotContain(c => c.ID == cageId);
    }

    [Fact]
    public void When_Schedule_WithValidValues_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDTO = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDTO).Result;

        var error = cageTimeBlockResponse.Content.ReadAsStringAsync().Result;
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<CageTimeBlock>().Result;
        cageTimeBlock.Should().NotBeNull();
        cageTimeBlock!.CageId.Should().Be(cageId);
        cageTimeBlock!.StartTime.Should().Be(start);
        cageTimeBlock!.EndTime.Should().Be(end);
        cageTimeBlock!.OccupantId.Should().Be(scheduleDTO.PetId);
    }

    [Fact]
    public void When_Schedule_WithBadPetId_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDTO = new ScheduleCageDTO
        {
            PetId = Guid.NewGuid(),
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDTO).Result;

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void When_Schedule_WithBadLocationId_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDTO = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = Guid.NewGuid(),
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDTO).Result;

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void When_Schedule_WithBadTime_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(-1);
        var scheduleDTO = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDTO).Result;

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void When_GetSheduleByLocation_WithValidId_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var cageTimeBlockId = SetupSchedule(petId, officeId, start, end);

        //Act
        var cageTimeBlockResponse = TestingClient.GetAsync($"{CagesUrl}/schedule/byLocation/{officeId}").Result;
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        //Assert
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>().Result;
        cageTimeBlock.Should().NotBeEmpty();
        cageTimeBlock.Should().HaveCount(1);
        cageTimeBlock.Should().Contain(c => c.Id == cageTimeBlockId);
    }

    [Fact]
    public void When_GetScheduleByPet_WithValidId_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = SetupOffice();
        var cageId = SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var cageTimeBlockId = SetupSchedule(petId, officeId, start, end);

        //Act
        var cageTimeBlockResponse = TestingClient.GetAsync($"{CagesUrl}/schedule/byPet/{petId}").Result;
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        //Assert
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>().Result;
        cageTimeBlock.Should().NotBeEmpty();
        cageTimeBlock.Should().HaveCount(1);
        cageTimeBlock.Should().Contain(c => c.Id == cageTimeBlockId);
    }

    [Fact]
    public void When_GetScheduleByLocation_WithInvalidId_ShouldReturnZeroCageTimeBlocks()
    {
        //Arrange
        ClearDatabase();
        //Act
        var cageTimeBlockResponse = TestingClient.GetAsync($"{CagesUrl}/schedule/byLocation/{Guid.NewGuid()}").Result;
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>().Result;
        cageTimeBlock.Should().BeEmpty();
    }

    [Fact]
    public void When_GetScheduleByPet_WithInvalidId_ShouldReturnZeroCageTimeBlocks()
    {
        //Arrange
        ClearDatabase();
        //Act
        var cageTimeBlockResponse = TestingClient.GetAsync($"{CagesUrl}/schedule/byPet/{Guid.NewGuid()}").Result;
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>().Result;
        cageTimeBlock.Should().BeEmpty();
    }

    private Guid SetupSchedule(Guid petId, Guid officeId, DateTime start, DateTime end)
    {
        var scheduleDTO = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        var cageTimeBlockResponse = TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDTO).Result;
        var cageTimeBlock = cageTimeBlockResponse.Content.ReadFromJsonAsync<CageTimeBlock>().Result;
        return cageTimeBlock!.Id;
    }

    private Guid SetupPet(Guid ownerId,string petSizeString)
    {
        var petDTO = new PetDTO
        {
            Name = "Test Pet",    
            DateOfBirth = DateTime.Now.AddYears(-1),
            Species = "Dog",
            Breed = "Labrador",
            OwnerId = ownerId,
            Size = petSizeString
        };

        var petResponse = TestingClient.PostAsJsonAsync(PetsUrl, petDTO).Result;
        petResponse.EnsureSuccessStatusCode();
        var pet = petResponse.Content.ReadFromJsonAsync<Pet>().Result;
        return pet!.Id;
    }

    private Guid SetupCage(Guid officeId, string size)
    {        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = size
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesUrl, sut).Result;

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
