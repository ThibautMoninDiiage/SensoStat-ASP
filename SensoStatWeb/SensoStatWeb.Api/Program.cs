using Microsoft.EntityFrameworkCore;
using SensoStatApi.Models;
using SensoStatApi.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SensoStatDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=SensoStat;User Id=UserSensoStat;Password=123"));

var context = builder.Services.BuildServiceProvider().GetRequiredService<SensoStatDbContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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

