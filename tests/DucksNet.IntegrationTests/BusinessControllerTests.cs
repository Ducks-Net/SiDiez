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
        result.Surname.Should().Be("Surname");
        result.FirstName.Should().Be("FirstName");
        result.Address.Should().Be("Address");
        result.OwnerPhone.Should().Be("0734567890");
        result.OwnerEmail.Should().Be("ion@e.com");
    }
    [Fact]
    public void When_Post_WithBadBusinessName_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeNull();
        errors[0].Should().Be("Business name is required");
    }
    [Fact]
    public void When_Post_WithBadSurname_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();
        errors[0].Should().Be("Surname is required");

    } 
    [Fact]
    public void When_Post_WithBadFirstName_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("First name is required");
    }
    [Fact] 
    public void When_Post_WithBadAddress_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("Address is required");

    }
    [Fact]
    public void When_Post_WithBadOwnerPhone_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("Owner phone is required");
    }
    [Fact]
    public void When_Post_WithBadOwnerEmail_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("Owner email is required");
    }
    [Fact]
    public void When_Post_WithBadOwnerEmail_Should_Fail2()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ion@e",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("Owner email is not valid");
    }
    [Fact]
    public void When_Post_WithBadOwnerEmail_Should_Fail3()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0734567890",
            OwnerEmail = "ione.",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors[0].Should().Be("Owner email is not valid");
    }

}
