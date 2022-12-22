using DucksNet.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DucksNet.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
     
        services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
        services.AddValidatorsFromAssemblyContaining<PetValidator>();
        services.AddValidatorsFromAssemblyContaining<MedicineValidator>();
        services.AddValidatorsFromAssemblyContaining<UserValidator>();
        return services;
    }
}
