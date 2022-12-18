using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
    public async Task When_Post_WithValidData_ShouldReturnCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = "Small"
        };

        //Act
        var cageResponse = await TestingClient.PostAsJsonAsync(CagesUrl, sut);

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = await cageResponse.Content.ReadFromJsonAsync<Cage>();
        cage.Should().NotBeNull();
        cage!.LocationId.Should().Be(officeId);
        cage!.Size.Should().Be(Size.Small);
    }

    [Fact]
    public async Task When_Post_WithBadLocation_ShouldReturnBadRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = new CreateCageDTO {
            LocationId = Guid.NewGuid(),
            SizeString = "Small"
        };

        //Act
        var cageResponse = await TestingClient.PostAsJsonAsync(CagesUrl, sut);

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await cageResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type Office was not found.");
    }

    [Fact]
    public async Task When_Post_WithBadSize_ShouldReturnBadRequest()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = "Invalid"
        };

        //Act
        var cageResponse = await TestingClient.PostAsJsonAsync(CagesUrl, sut);

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await cageResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Failed to parse cage size.");
    }

    [Fact]
    public async Task When_Get_WithValidId_ShouldReturnCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = await TestingClient.GetAsync($"{CagesUrl}/{cageId}");

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = await cageResponse.Content.ReadFromJsonAsync<Cage>();
        cage.Should().NotBeNull();
        cage!.ID.Should().Be(cageId);
        cage!.LocationId.Should().Be(officeId);
        cage!.Size.Should().Be(Size.Small);
    }

    [Fact]
    public async Task When_Get_WithInvalidId_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = await TestingClient.GetAsync($"{CagesUrl}/{Guid.NewGuid()}");

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_GetByLocation_WithValidLocation_ShouldReturnCages()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = await TestingClient.GetAsync($"{CagesUrl}/byLocation/{officeId}");

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = await cageResponse.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().NotBeEmpty();
        cages.Should().Contain(c => c.ID == cageId);
    }

    [Fact]
    public async Task When_GetByLocation_ShouldReturnOnlyFromThatLocation()
    {
        ClearDatabase();
        //Arrange
        var officeId  = await SetupOffice();
        var cageId    = await SetupCage(officeId, Size.Small.Name);
        var officeId2 = await SetupOffice();
        var cageId2   = await SetupCage(officeId2, Size.Small.Name);
        
        //Act
        var cageResponse = await TestingClient.GetAsync($"{CagesUrl}/byLocation/{officeId}");

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = await cageResponse.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().NotBeEmpty();
        cages.Should().Contain(c => c.ID == cageId);
        cages.Should().NotContain(c => c.ID == cageId2);
    }

    [Fact]
    public async Task When_GetByLocation_WithInvalidLocation_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = await TestingClient.GetAsync($"{CagesUrl}/byLocation/{Guid.NewGuid()}");

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_GetAll_Should_ReturnAllValues()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        var cageId2 = await SetupCage(officeId, Size.Medium.Name);
        
        //Act
        var cageResponse = await TestingClient.GetAsync(CagesUrl);

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = await cageResponse.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().NotBeEmpty();
        cages.Should().HaveCount(2);
        cages.Should().Contain(c => c.ID == cageId);
        cages.Should().Contain(c => c.ID == cageId2);
    }

    [Fact]
    public async Task When_GetAll_Should_ReturnEmptyList()
    {
        ClearDatabase();
        //Arrange
        
        //Act
        var cageResponse = await TestingClient.GetAsync(CagesUrl);

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cages = await cageResponse.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().BeEmpty();
    }

    [Fact]
    public async Task When_Delete_WithValidId_ShouldDeleteCage()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        
        //Act
        var cageResponse = await TestingClient.DeleteAsync($"{CagesUrl}/{cageId}");

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cagesR = await TestingClient.GetAsync(CagesUrl);
        var cages = await cagesR.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().BeEmpty();
    }

    [Fact]
    public async Task When_Delete_WithInvalidId_ShouldReturnNotFound()
    {
        //Arrange
        ClearDatabase();
        
        //Act
        var cageResponse = await TestingClient.DeleteAsync($"{CagesUrl}/{Guid.NewGuid()}");

        //Assert
        cageResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_Delete_ShouldOnlyDeleteSelected()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        var cageId2 = await SetupCage(officeId, Size.Medium.Name);
        
        //Act
        var cageResponse = await TestingClient.DeleteAsync($"{CagesUrl}/{cageId}");

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cagesR = await TestingClient.GetAsync(CagesUrl); 
        var cages = await cagesR.Content.ReadFromJsonAsync<List<Cage>>();
        cages.Should().NotBeEmpty();
        cages.Should().HaveCount(1);
        cages.Should().Contain(c => c.ID == cageId2);
        cages.Should().NotContain(c => c.ID == cageId);
    }

    [Fact]
    public async Task When_Schedule_WithValidValues_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDto = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = await TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDto);

        var error = await cageTimeBlockResponse.Content.ReadAsStringAsync();
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<CageTimeBlock>();
        cageTimeBlock.Should().NotBeNull();
        cageTimeBlock!.CageId.Should().Be(cageId);
        cageTimeBlock!.StartTime.Should().Be(start);
        cageTimeBlock!.EndTime.Should().Be(end);
        cageTimeBlock!.OccupantId.Should().Be(scheduleDto.PetId);
    }

    [Fact]
    public async Task When_Schedule_WithBadPetId_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDto = new ScheduleCageDTO
        {
            PetId = Guid.NewGuid(),
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = await TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDto);

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task When_Schedule_WithBadLocationId_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var scheduleDto = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = Guid.NewGuid(),
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = await TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDto);

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task When_Schedule_WithBadTime_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(-1);
        var scheduleDto = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        
        //Act
        var cageTimeBlockResponse = await TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDto);

        //Assert
        cageTimeBlockResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task When_GetScheduleByLocation_WithValidId_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var cageTimeBlockId = await SetupSchedule(petId, officeId, start, end);

        //Act
        var cageTimeBlockResponse = await TestingClient.GetAsync($"{CagesUrl}/schedule/byLocation/{officeId}");
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        //Assert
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>();
        cageTimeBlock.Should().NotBeEmpty();
        cageTimeBlock.Should().HaveCount(1);
        cageTimeBlock.Should().Contain(c => c.Id == cageTimeBlockId);
    }

    [Fact]
    public async Task When_GetScheduleByPet_WithValidId_ShouldReturnCageTimeBlock()
    {
        ClearDatabase();
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Small.Name);
        var officeId = await SetupOffice();
        var cageId = await SetupCage(officeId, Size.Small.Name);
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = DateTime.Now.AddDays(1).AddHours(1);
        var cageTimeBlockId = await SetupSchedule(petId, officeId, start, end);

        //Act
        var cageTimeBlockResponse = await TestingClient.GetAsync($"{CagesUrl}/schedule/byPet/{petId}");
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        //Assert
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>();
        cageTimeBlock.Should().NotBeEmpty();
        cageTimeBlock.Should().HaveCount(1);
        cageTimeBlock.Should().Contain(c => c.Id == cageTimeBlockId);
    }

    [Fact]
    public async Task When_GetScheduleByLocation_WithInvalidId_ShouldReturnZeroCageTimeBlocks()
    {
        //Arrange
        ClearDatabase();
        //Act
        var cageTimeBlockResponse = await TestingClient.GetAsync($"{CagesUrl}/schedule/byLocation/{Guid.NewGuid()}");
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>();
        cageTimeBlock.Should().BeEmpty();
    }

    [Fact]
    public async Task When_GetScheduleByPet_WithInvalidId_ShouldReturnZeroCageTimeBlocks()
    {
        //Arrange
        ClearDatabase();
        //Act
        var cageTimeBlockResponse = await TestingClient.GetAsync($"{CagesUrl}/schedule/byPet/{Guid.NewGuid()}");
        //Assert
        cageTimeBlockResponse.EnsureSuccessStatusCode();
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<List<CageTimeBlock>>();
        cageTimeBlock.Should().BeEmpty();
    }

    private async Task<Guid> SetupSchedule(Guid petId, Guid officeId, DateTime start, DateTime end)
    {
        var scheduleDto = new ScheduleCageDTO
        {
            PetId = petId,
            LocationId = officeId,
            StartTime = start,
            EndTime = end
        };
        var cageTimeBlockResponse = await TestingClient.PostAsJsonAsync($"{CagesUrl}/schedule", scheduleDto);
        var cageTimeBlock = await cageTimeBlockResponse.Content.ReadFromJsonAsync<CageTimeBlock>();
        return cageTimeBlock!.Id;
    }

    private async Task<Guid> SetupPet(Guid ownerId,string petSizeString)
    {
        var petDto = new PetDTO
        {
            Name = "Test Pet",    
            DateOfBirth = DateTime.Now.AddYears(-1),
            Species = "Dog",
            Breed = "Labrador",
            OwnerId = ownerId,
            Size = petSizeString
        };

        var petResponse = await TestingClient.PostAsJsonAsync(PetsUrl, petDto);
        petResponse.EnsureSuccessStatusCode();
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        return pet!.Id;
    }

    private async Task<Guid> SetupCage(Guid officeId, string size)
    {        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = size
        };

        //Act
        var cageResponse = await TestingClient.PostAsJsonAsync(CagesUrl, sut);

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = await cageResponse.Content.ReadFromJsonAsync<Cage>();
        return cage!.ID;
    }

    private async Task<Guid> SetupOffice()
    {
        var officeDto = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var officeResponse = await TestingClient.PostAsJsonAsync(OfficesUrl, officeDto);
        officeResponse.EnsureSuccessStatusCode();
        var office = await officeResponse.Content.ReadFromJsonAsync<Office>();
        office.Should().NotBeNull();
        return office!.ID;
    }
}
