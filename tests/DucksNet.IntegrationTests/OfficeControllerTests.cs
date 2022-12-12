using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace  DucksNet.IntegrationTests;

public class OfficeControllerTests : BaseIntegrationTests<OfficeController>
{
    private const string OfficesUrl = "api/v1/office";
    [Fact]
    public void When_Office_Create_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = TestingClient.PostAsJsonAsync(OfficesUrl, office).Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public void When_Office_CreateWithEmptyAddress_Should_ReturnBadRequest()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = string.Empty,
            AnimalCapacity = 10
        };
        var response = TestingClient.PostAsJsonAsync(OfficesUrl, office).Result;
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public void When_Office_Get_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = TestingClient.PostAsJsonAsync(OfficesUrl, office).Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var officeId = response.Content.ReadFromJsonAsync<Office>().Result!.ID;
        var getResponse = TestingClient.GetAsync($"{OfficesUrl}/{officeId}").Result;
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public void When_Office_Get_Should_ReturnNotFound()
    {
        var officeId = Guid.NewGuid();
        var getResponse = TestingClient.GetAsync($"{OfficesUrl}/{officeId}").Result;
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public void When_Office_GetAll_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = TestingClient.PostAsJsonAsync(OfficesUrl, office).Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var getResponse = TestingClient.GetAsync(OfficesUrl).Result;
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public void When_Office_Delete_Should_ReturnOk()
    {
        var office = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Adresa",
            AnimalCapacity = 10
        };
        var response = TestingClient.PostAsJsonAsync(OfficesUrl, office).Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var officeId = response.Content.ReadFromJsonAsync<Office>().Result!.ID;
        var deleteResponse = TestingClient.DeleteAsync($"{OfficesUrl}/{officeId}").Result;
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public void When_Office_Delete_Should_ReturnNotFound()
    {
        var officeId = Guid.NewGuid();
        var deleteResponse = TestingClient.DeleteAsync($"{OfficesUrl}/{officeId}").Result;
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
}
