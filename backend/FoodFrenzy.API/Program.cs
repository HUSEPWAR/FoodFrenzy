using FoodFrenzy.Application.Security;
using FoodFrenzy.Infrastructure.Persistence;
using FoodFrenzy.Infrastructure.Repositories;
using FoodFrenzy.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using FoodFrenzy.API.Exceptions;
using FoodFrenzy.Application.Users.Registration;
using FoodFrenzy.Application.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();

builder.Services.AddDbContext<FoodFrenzyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodFrenzyDb")));



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();