using Microsoft.EntityFrameworkCore;
using SportsApi.Models;
using SportsApi.Contracts;
using SportsApi.Services;
using SportsApi.Contexts;
using SportsApi.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SportsContext>(opt =>
    opt.UseInMemoryDatabase("SportsDb"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataService<Team>, EfService<Team>>();
builder.Services.AddScoped<IDataService<Player>, EfService<Player>>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

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
