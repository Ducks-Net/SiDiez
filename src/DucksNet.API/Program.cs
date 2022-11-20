using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.Infrastructure.Prelude;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IRepository<Cage>, CagesRepository>();
builder.Services.AddScoped<IRepository<CageTimeBlock>, CageTimeBlocksRepository>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentsRepository>();
builder.Services.AddScoped<IRepository<Pet>, PetsRepository>();
builder.Services.AddScoped<IRepository<User>, UsersRepository>();

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
