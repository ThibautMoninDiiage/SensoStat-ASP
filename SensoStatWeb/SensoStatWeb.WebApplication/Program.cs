using SensoStatWeb.Business;
using SensoStatWeb.Business.Interfaces;

var builder = WebApplication.CreateBuilder(args); // create web app
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Add controler with views to app

builder.Services.AddScoped<IHttpService, HttpService>();

var app = builder.Build();
app.UseRouting(); // Start
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.UseStaticFiles();

app.Run();

