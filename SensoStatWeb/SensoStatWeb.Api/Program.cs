using Microsoft.EntityFrameworkCore;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository;
using SensoStatWeb.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdministratorRepository,DbAdministratorRepository>();
builder.Services.AddScoped<ISurveyRepository,DbSurveyRepository>();
builder.Services.AddScoped<IInstructionRepository,DbInstructionRepository>();
builder.Services.AddScoped<IQuestionRepository,DbQuestionRepository>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<ISurveyStateRepository, DbSurveyStateRepository>();
builder.Services.AddScoped<IProductRepository, DbProductRepository>();
builder.Services.AddScoped<IUserProductRepository, DbUserProductRepository>();
string connexion = "";

#if DEBUG
    connexion = builder.Configuration.GetConnectionString("local");
    builder.Services.AddDbContext<SensoStatDbContext>(options => options.UseSqlServer(connexion));
#endif

if (connexion == "")
{
    connexion = builder.Configuration.GetConnectionString("sqlAzure");
    builder.Services.AddDbContext<SensoStatDbContext>(options => options.UseSqlServer(connexion));
}

var context = builder.Services.BuildServiceProvider().GetRequiredService<SensoStatDbContext>();
//context.Database.EnsureDeleted();
//context.Database.EnsureCreated();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();

