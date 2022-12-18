using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;
using FluentValidation.AspNetCore;
using System.Reflection;
using DucksNet.API.Validators;
using FluentValidation;
using DucksNet.API.Mappers;
using DucksNet.Infrastructure.SqliteAsync;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IRepositoryAsync<Cage>, RepositoryAsync<Cage>>();
builder.Services.AddScoped<IRepositoryAsync<CageTimeBlock>, RepositoryAsync<CageTimeBlock>>();
builder.Services.AddScoped<IRepositoryAsync<Appointment>, RepositoryAsync<Appointment>>();
builder.Services.AddScoped<IRepositoryAsync<Pet>, RepositoryAsync<Pet>>();
builder.Services.AddScoped<IRepositoryAsync<User>, RepositoryAsync<User>>();
builder.Services.AddScoped<IRepositoryAsync<Treatment>, RepositoryAsync<Treatment>>();
builder.Services.AddScoped<IRepositoryAsync<Medicine>, RepositoryAsync<Medicine>>();
builder.Services.AddScoped<IRepositoryAsync<MedicalRecord>, RepositoryAsync<MedicalRecord>>();
builder.Services.AddScoped<IRepositoryAsync<Employee>, RepositoryAsync<Employee>>();
builder.Services.AddScoped<IRepositoryAsync<Business>, RepositoryAsync<Business>>();
builder.Services.AddScoped<IRepositoryAsync<Office>, RepositoryAsync<Office>>();


builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PetValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MedicineValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();


builder.Services.AddAutoMapper(typeof(CageMappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("VetPolicyCors",policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("VetPolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();

public abstract partial class Program
{
    // Expose the Program class for use with WebApplicationFactory<T>
    protected Program() {}
}
