using DeepFreez.WebApp.Data;
using DeepFreez.WebApp.Helper;
using DeepFreez.WebApp.Model.SettingModels;
using DeepFreez.WebApp.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.Configure<DeepFreesDatabaseSettings>(builder.Configuration.GetSection("DeepFreesDatabase"));
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeDataContext>();
builder.Services.AddScoped<CallPoolDataContext>();
builder.Services.AddScoped<CallCenterService>();
builder.Services.AddScoped<DispatchService>();
builder.Services.AddScoped<WorkTaskService>();
builder.Services.AddScoped<DispJobsDataContext>();
builder.Services.AddScoped<EmpJobsDataContext>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<CallCenterDataContext>();
builder.Services.AddScoped<WorkTaskRequestDataService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
