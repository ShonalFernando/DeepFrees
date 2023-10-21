using DeepFrees.PayrollServices.MicroService;
using Hangfire;
using System.Data.SqlClient;

using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(x => x.UseSqlServerStorage((@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DFPRJobTasks;Integrated Security=True;Connect Timeout=30;Encrypt=False")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<PaySheetGenerator>();
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
