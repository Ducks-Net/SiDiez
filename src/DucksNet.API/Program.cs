﻿using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IRepository<Treatment>, TreatmentsRepository>();
builder.Services.AddScoped<IRepository<Medicine>, MedicinesRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

builder.Services.AddScoped<IRepository<MedicalRecord>, MedicalRecordRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();

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
