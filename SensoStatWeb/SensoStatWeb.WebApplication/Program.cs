using SensoStatWeb.Business;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.WebApplication.Services;
using SensoStatWeb.WebApplication.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args); // create web app
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Add controler with views to app

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();


var app = builder.Build();
app.UseRouting(); // Start
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "",
        defaults: new { controller = "login", action = "index" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}
);
app.UseStaticFiles();

app.Run();

