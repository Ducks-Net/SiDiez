using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;
using FluentValidation.AspNetCore;
using System.Reflection;
using DucksNet.API.Validators;
using FluentValidation;
using DucksNet.API.Mappers;
using DucksNet.Infrastructure.SqliteAsync;
using DucksNet.Infrastructure;
using DucksNet.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("VetPolicyCors",policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddAutoMapper(typeof(CageMappingProfile).Assembly);


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
