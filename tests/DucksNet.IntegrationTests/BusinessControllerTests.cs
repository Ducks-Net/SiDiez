using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;

namespace  DucksNet.IntegrationTests;

public class BusinessControllerTests : BaseIntegrationTests<BusinessController>
{
    private const string BusinessUrl = "api/v1/business";

    [Fact]
    public async Task When_BusinessCreated_Should_Succeed()
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
        
        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Business>();
        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result!.Surname.Should().Be(business.Surname);
        result!.FirstName.Should().Be(business.FirstName);
        result!.Address.Should().Be(business.Address);
        result!.OwnerPhone.Should().Be(business.OwnerPhone);
        result!.OwnerEmail.Should().Be(business.OwnerEmail);
    }
    [Fact]
    public async Task When_Post_WithBadBusinessName_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeNull();
        errors![0].Should().Be("Business name is required");
    }
    [Fact]
    public async Task When_Post_WithBadSurname_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();
        errors![0].Should().Be("Surname is required");
    } 
    [Fact]
    public async Task When_Post_WithBadFirstName_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors![0].Should().Be("First name is required");
    }
    [Fact] 
    public async Task When_Post_WithBadAddress_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors![0].Should().Be("Address is required");
    }
    [Fact]
    public async Task When_Post_WithBadOwnerPhone_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors![0].Should().Be("Owner phone is required");
    }
    [Fact]
    public async Task When_Post_WithBadOwnerEmail_Should_Fail()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors![0].Should().Be("Owner email is required");
    }
    [Fact]
    public async Task When_Post_WithBadOwnerEmail_Should_Fail2()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors![0].Should().Be("Owner email is invalid");
    }
    [Fact]
    public async Task When_Get_Should_ReturnBusiness()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result.Surname.Should().Be(business.Surname);
        result.FirstName.Should().Be(business.FirstName);
        result.Address.Should().Be(business.Address);
        result.OwnerPhone.Should().Be(business.OwnerPhone);
        result.OwnerEmail.Should().Be(business.OwnerEmail);
    }
    [Fact] 
    public async Task When_GetAll_Should_ReturnAllValues()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be(business.BusinessName);
        result.Surname.Should().Be(business.Surname);
        result.FirstName.Should().Be(business.FirstName);
        result.Address.Should().Be(business.Address);
        result.OwnerPhone.Should().Be(business.OwnerPhone);
        result.OwnerEmail.Should().Be(business.OwnerEmail);
    }
    [Fact]
    public async Task When_GetAll_Should_ReturnAllValues2()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

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

        var response2 = await TestingClient.PostAsJsonAsync(BusinessUrl, business2);

        response2.StatusCode.Should().Be(HttpStatusCode.OK);
        var result2 = await response2.Content.ReadFromJsonAsync<Business>();

        result2.Should().NotBeNull();

        var response3 = await TestingClient.GetAsync(BusinessUrl);

        response3.StatusCode.Should().Be(HttpStatusCode.OK);

        var result3 = await response3.Content.ReadFromJsonAsync<List<Business>>();

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
    public async Task When_GetAll_Should_ReturnAllValues3()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

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

        var response2 = await TestingClient.PostAsJsonAsync(BusinessUrl, business2);

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response2.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors!.Count.Should().Be(1);
        errors![0].Should().Be("Owner email is required");
    }
    [Fact]
    public async Task When_GetAll_Should_ReturnAllValues4()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

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

        var response2 = await TestingClient.PostAsJsonAsync(BusinessUrl, business2);

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response2.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors!.Count.Should().Be(1);

        errors[0].Should().Be("Owner email is invalid");
    }
    [Fact]
    public async Task When_GetAll_Should_ReturnAllValues6()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

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

        var response2 = await TestingClient.PostAsJsonAsync(BusinessUrl, business2);

        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response2.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors!.Count.Should().Be(1);

        errors[0].Should().Be("Owner email is invalid");
    }
    private async Task<Guid> GetBusinessId()
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

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<Business>();

        result.Should().NotBeNull();

        return result!.ID;
    }
    [Fact]
    public async Task When_Delete_ShouldOnlyDeleteSelected()
    {
        ClearDatabase();
        var businessId  = await GetBusinessId();
        var businessId2 = await GetBusinessId();
        var businessId3 = await GetBusinessId();

        var response = await TestingClient.DeleteAsync($"{BusinessUrl}/{businessId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var response2 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId}");

        response2.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response3 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId2}");

        response3.StatusCode.Should().Be(HttpStatusCode.OK);

        var response4 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId3}");

        response4.StatusCode.Should().Be(HttpStatusCode.OK);

        var response5 = await TestingClient.DeleteAsync($"{BusinessUrl}/{businessId2}");

        response5.StatusCode.Should().Be(HttpStatusCode.OK);

        var response6 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId2}");

        response6.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response7 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId3}");

        response7.StatusCode.Should().Be(HttpStatusCode.OK);

        var response8 = await TestingClient.DeleteAsync($"{BusinessUrl}/{businessId3}");

        response8.StatusCode.Should().Be(HttpStatusCode.OK);

        var response9 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId3}");

        response9.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response10 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId}");

        response10.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response11 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId2}");

        response11.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response12 = await TestingClient.GetAsync($"{BusinessUrl}/{businessId3}");

        response12.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var response13 = await TestingClient.GetAsync(BusinessUrl);

        response13.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response13.Content.ReadFromJsonAsync<List<Business>>();

        result.Should().NotBeNull();

        result!.Count.Should().Be(0);
    }

    [Fact]
    public async Task When_Get_Should_ReturnValue()
    {
        ClearDatabase();
        var businessId = await GetBusinessId();
        var response = await TestingClient.GetAsync($"{BusinessUrl}/{businessId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Business>();

        result.Should().NotBeNull();

        result!.ID.Should().Be(businessId);
    }
    [Fact]
    public async Task When_Create_Should_Fail()
    {
        ClearDatabase();
        var business = new BusinessDTO
        {
            BusinessName = "BusinessName",
            Surname = "",
            FirstName = "FirstName",
            Address = "Address",
            OwnerPhone = "0731678902",
            OwnerEmail = "asd@asd.com",
        };

        var response = await TestingClient.PostAsJsonAsync(BusinessUrl, business);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        errors.Should().NotBeNull();

        errors!.Count.Should().Be(1);

        errors[0].Should().Be("Surname is required");

    }
    [Fact]
    public async Task When_Delete_Should_Fail()
    {
        ClearDatabase();
        var response = await TestingClient.DeleteAsync($"{BusinessUrl}/00000000-0000-0000-0000-000000000000");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}       
