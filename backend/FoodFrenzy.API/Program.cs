using FoodFrenzy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FoodFrenzyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodFrenzyDb")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();