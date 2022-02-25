using Microsoft.EntityFrameworkCore;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Models.Seeder;
using SensoStatWeb.Repository;
using SensoStatWeb.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdministratorRepository,DbAdministratorRepository>();
builder.Services.AddScoped<ISurveyRepository,DbSurveyRepository>();
builder.Services.AddScoped<IInstructionRepository,DbInstructionRepository>();
builder.Services.AddScoped<IQuestionRepository,DbQuestionRepository>();

builder.Services.AddDbContext<SensoStatDbContext>(options => options.UseSqlServer("Server = tcp:sensostatg1.database.windows.net, 1433; Initial Catalog = Sensostat; Persist Security Info=False; User ID = senso; Password = Deviceqrtmtbtdbr1.; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"));
//builder.Services.AddDbContext<SensoStatDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=SensoStat;User Id=UserSensoStat;Password=123"));

var context = builder.Services.BuildServiceProvider().GetRequiredService<SensoStatDbContext>();
//context.Database.EnsureDeleted();
//context.Database.EnsureCreated();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // SeedData.Initialize(services);
}

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

