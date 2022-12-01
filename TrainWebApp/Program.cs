using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using TrainWatchSystem;
using TrainWatchSystem.BLL;
using TrainWatchSystem.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Setup connection string service for the application. 
//1) Retrieve the connection string information from your appsettings.json

var connectionString = builder.Configuration.GetConnectionString("TrainWatchDatabase");

//2) Register any services you wish to use. 
// In our soltuion our service will be created/coded in the class library WestWindSystem.
// One of these services will be the setup of the database context connection. 

// This setup can be done here locally or can also be done elsewhere and called from this location. 

builder.Services.AddBackendDependencies(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
