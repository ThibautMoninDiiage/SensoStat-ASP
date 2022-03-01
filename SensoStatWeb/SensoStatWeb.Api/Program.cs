using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SensoStatWeb.Repository.Interfaces;

#region Builder

var builder = WebApplication.CreateBuilder(args);

#region Cors

builder.Services.AddCors();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ent�tes d'autorisation JWT \r\n\r\n Tapez 'Bearer' [espace] et votre token dans l'input qui suis.\r\n\r\nExemple: \"Bearer 1safsfsdfdfd\"",
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
           new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             new string[] {}
        }
    });
});

#endregion

#region Jwt

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
});

#endregion

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region IOC

builder.Services.AddScoped<IAdministratorRepository, DbAdministratorRepository>();
builder.Services.AddScoped<ISurveyRepository, DbSurveyRepository>();
builder.Services.AddScoped<IInstructionRepository, DbInstructionRepository>();
builder.Services.AddScoped<IQuestionRepository, DbQuestionRepository>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<ISurveyStateRepository, DbSurveyStateRepository>();
builder.Services.AddScoped<IProductRepository, DbProductRepository>();
builder.Services.AddScoped<IUserProductRepository, DbUserProductRepository>();

#endregion

#region Database / DbContext

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

#endregion

#endregion

#region App

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
