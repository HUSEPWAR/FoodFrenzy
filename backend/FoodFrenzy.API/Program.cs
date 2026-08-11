using FoodFrenzy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FoodFrenzy.Application.Interfaces;
using FoodFrenzy.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<FoodFrenzyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodFrenzyDb")));



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();