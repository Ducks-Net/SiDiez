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

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = response.Content.ReadFromJsonAsync<Business>().Result;
        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result!.Surname.Should().Be(business.Surname);
        result!.FirstName.Should().Be(business.FirstName);
        result!.Address.Should().Be(business.Address);
        result!.OwnerPhone.Should().Be(business.OwnerPhone);
        result!.OwnerEmail.Should().Be(business.OwnerEmail);
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

        errors[0].Should().Be("Owner email is invalid");
    }
    [Fact]
    public void When_Get_Should_ReturnBusiness()
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

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result.Surname.Should().Be(business.Surname);
        result.FirstName.Should().Be(business.FirstName);
        result.Address.Should().Be(business.Address);
        result.OwnerPhone.Should().Be(business.OwnerPhone);
        result.OwnerEmail.Should().Be(business.OwnerEmail);
    }
    [Fact] 
    public void When_GetAll_Should_ReturnAllValues()
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

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result.Surname.Should().Be(business.Surname);
        result.FirstName.Should().Be(business.FirstName);
        result.Address.Should().Be(business.Address);
        result.OwnerPhone.Should().Be(business.OwnerPhone);
        result.OwnerEmail.Should().Be(business.OwnerEmail);
    }
    [Fact]
    public void When_GetAll_Should_ReturnAllValues2()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "ion@e.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        var business2 = new BusinessDTO
        {
            BusinessName = "BusinessName2",
            Surname = "Surname2",
            FirstName = "FirstName2",
            Address = "Address2",
            OwnerPhone = "0734678902",
            OwnerEmail = "aaasd@i.com",
        };

        var response2 =  TestingClient.PostAsJsonAsync(businessUrl, business2).Result;

        response2.StatusCode.Should().Be(HttpStatusCode.OK);
        var result2 = response2.Content.ReadFromJsonAsync<Business>().Result;

        result2.Should().NotBeNull();

        var response3 =  TestingClient.GetAsync(businessUrl).Result;

        response3.StatusCode.Should().Be(HttpStatusCode.OK);

        var result3 = response3.Content.ReadFromJsonAsync<List<Business>>().Result;

        result3.Should().NotBeNull();
        result3!.Count.Should().Be(2);
        result3[0].BusinessName.Should().Be(business.BusinessName);
        result3[0].Surname.Should().Be(business.Surname);
        result3[0].FirstName.Should().Be(business.FirstName);
        result3[0].Address.Should().Be(business.Address);
        result3[0].OwnerPhone.Should().Be(business.OwnerPhone);
        result3[0].OwnerEmail.Should().Be(business.OwnerEmail);
        result3[1].BusinessName.Should().Be(business2.BusinessName);
        result3[1].Surname.Should().Be(business2.Surname);
        result3[1].FirstName.Should().Be(business2.FirstName);
        result3[1].Address.Should().Be(business2.Address);
        result3[1].OwnerPhone.Should().Be(business2.OwnerPhone);
        result3[1].OwnerEmail.Should().Be(business2.OwnerEmail);
    }
    [Fact]
    public void When_GetAll_Should_ReturnAllValues3()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "asd@asd.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        var business2 = new BusinessDTO
        {
            BusinessName = "BusinessName2",
            Surname = "Surname2",
            FirstName = "FirstName2",
            Address = "Address2",
            OwnerPhone = "0734678902",
            OwnerEmail = "",
        
        };

        var response2 =  TestingClient.PostAsJsonAsync(businessUrl, business2).Result;

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response2.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors.Count.Should().Be(1);
        errors[0].Should().Be("Owner email is required");
    }
    [Fact]
    public void When_GetAll_Should_ReturnAllValues4()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "asd@a.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        var business2 = new BusinessDTO
        {
            BusinessName = "BusinessName2",
            Surname = "Surname2",
            FirstName = "FirstName2",
            Address = "Address2",
            OwnerPhone = "0734678902",
            OwnerEmail = "asedasd",
        };

        var response2 =  TestingClient.PostAsJsonAsync(businessUrl, business2).Result;

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response2.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors.Count.Should().Be(1);

        errors[0].Should().Be("Owner email is invalid");
    }
    [Fact]
    public void When_GetAll_Should_ReturnAllValues5()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "a@asd.comasd",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        var business2 = new BusinessDTO
        {
            BusinessName = "BusinessName2",
            Surname = "Surname2",
            FirstName = "FirstName2",
            Address = "Address2",
            OwnerPhone = "0734678902",
            OwnerEmail = "asd.asd",
        };

        var response2 =  TestingClient.PostAsJsonAsync(businessUrl, business2).Result;

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response2.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors.Count.Should().Be(1);

        errors[0].Should().Be("Owner email is invalid");
    }
    [Fact]
    public void When_GetAll_Should_ReturnAllValues6()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "a@a.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        var business2 = new BusinessDTO
        {
            BusinessName = "BusinessName2",
            Surname = "Surname2",
            FirstName = "FirstName2",
            Address = "Address2",
            OwnerPhone = "0734678902",
            OwnerEmail = "asd@asd",
        };

        var response2 =  TestingClient.PostAsJsonAsync(businessUrl, business2).Result;

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = response2.Content.ReadFromJsonAsync<List<string>>().Result;

        errors.Should().NotBeNull();

        errors.Count.Should().Be(1);

        errors[0].Should().Be("Owner email is invalid");
    }
    private Guid GetBusinessId()
    {
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "Surname",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "asd@asd.com",
        };

        var response =  TestingClient.PostAsJsonAsync(businessUrl, business).Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

    
        var result = response.Content.ReadFromJsonAsync<Business>().Result;

        result.Should().NotBeNull();

        return result!.ID;
    }
    [Fact]
    public void When_Delete_ShouldOnlyDeleteSelected()
    {
        ClearDatabase();
        var businessId = GetBusinessId();
        var businessId2 = GetBusinessId();
        var businessId3 = GetBusinessId();

        var response =  TestingClient.DeleteAsync($"{businessUrl}/{businessId}").Result;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var response2 =  TestingClient.GetAsync($"{businessUrl}/{businessId}").Result;

        response2.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response3 =  TestingClient.GetAsync($"{businessUrl}/{businessId2}").Result;

        response3.StatusCode.Should().Be(HttpStatusCode.OK);

        var response4 =  TestingClient.GetAsync($"{businessUrl}/{businessId3}").Result;

        response4.StatusCode.Should().Be(HttpStatusCode.OK);

        var response5 =  TestingClient.DeleteAsync($"{businessUrl}/{businessId2}").Result;

        response5.StatusCode.Should().Be(HttpStatusCode.OK);

        var response6 =  TestingClient.GetAsync($"{businessUrl}/{businessId2}").Result;

        response6.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response7 =  TestingClient.GetAsync($"{businessUrl}/{businessId3}").Result;

        response7.StatusCode.Should().Be(HttpStatusCode.OK);

        var response8 =  TestingClient.DeleteAsync($"{businessUrl}/{businessId3}").Result;

        response8.StatusCode.Should().Be(HttpStatusCode.OK);

        var response9 =  TestingClient.GetAsync($"{businessUrl}/{businessId3}").Result;

        response9.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response10 =  TestingClient.GetAsync($"{businessUrl}/{businessId}").Result;

        response10.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response11 =  TestingClient.GetAsync($"{businessUrl}/{businessId2}").Result;

        response11.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response12 =  TestingClient.GetAsync($"{businessUrl}/{businessId3}").Result;

        response12.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response13 =  TestingClient.GetAsync(businessUrl).Result;

        response13.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = response13.Content.ReadFromJsonAsync<List<Business>>().Result;

        result.Should().NotBeNull();

        result.Count.Should().Be(0);

    }
}       
