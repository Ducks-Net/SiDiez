using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IRepository<Cage>, CagesRepository>();
builder.Services.AddScoped<IRepository<CageTimeBlock>, CageTimeBlocksRepository>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentsRepository>();
builder.Services.AddScoped<IRepository<Pet>, PetsRepository>();
builder.Services.AddScoped<IRepository<User>, UsersRepository>();
builder.Services.AddScoped<IRepository<Treatment>, TreatmentsRepository>();
builder.Services.AddScoped<IRepository<Medicine>, MedicinesRepository>();
builder.Services.AddScoped<IRepository<MedicalRecord>, MedicalRecordRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IRepository<Business>, BusinessesRepository>();
builder.Services.AddScoped<IRepository<Office>, OfficesRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
