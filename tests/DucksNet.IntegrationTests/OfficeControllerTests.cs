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

public class OfficeControllerTests : BaseIntegrationTests<OfficeController>
{
    private const string OfficesUrl = "api/v1/office";
    [Fact]
    public async Task When_Office_Create_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = await TestingClient.PostAsJsonAsync(OfficesUrl, office);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task When_Office_CreateWithEmptyAddress_Should_ReturnBadRequest()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = string.Empty,
            AnimalCapacity = 10
        };
        var response = await TestingClient.PostAsJsonAsync(OfficesUrl, office);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task When_Office_Get_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = await TestingClient.PostAsJsonAsync(OfficesUrl, office);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var officeJson = await response.Content.ReadFromJsonAsync<Office>();
        var officeId = officeJson!.ID;
        var getResponse = await TestingClient.GetAsync($"{OfficesUrl}/{officeId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task When_Office_Get_Should_ReturnNotFound()
    {
        var officeId = Guid.NewGuid();
        var getResponse = await TestingClient.GetAsync($"{OfficesUrl}/{officeId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task When_Office_GetAll_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = await TestingClient.PostAsJsonAsync(OfficesUrl, office);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var getResponse = await TestingClient.GetAsync(OfficesUrl);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task When_Office_Delete_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = await TestingClient.PostAsJsonAsync(OfficesUrl, office);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var officeJson = await response.Content.ReadFromJsonAsync<Office>();
        var officeId = officeJson!.ID;
        var deleteResponse = await TestingClient.DeleteAsync($"{OfficesUrl}/{officeId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task When_Office_Delete_Should_ReturnNotFound()
    {
        var officeId = Guid.NewGuid();
        var deleteResponse = await TestingClient.DeleteAsync($"{OfficesUrl}/{officeId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
