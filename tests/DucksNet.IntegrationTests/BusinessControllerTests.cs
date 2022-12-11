using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace  DucksNet.IntegrationTests;

public class BusinessControllerTests : BaseIntegrationTests<BusinessController>
{
    private const string businessUrl = "api/v1/business";

    [Fact]
    public void When_BusinessCreated_Should_Succeed()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e.com",
        };
        
        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.EnsureSuccessStatusCode();
        var result = response.Content.ReadFromJsonAsync<Business>().Result;
        result.Should().NotBeNull();
        result!.BusinessName.Should().Be("BusinessName");

    }

}
