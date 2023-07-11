using Microsoft.EntityFrameworkCore;

using GenericProject.Model;
using GenericProject.Business;
using GenericProject.Business.Implementations;
using GenericProject.Repository;
using GenericProject.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
var connectionString = "Server=localhost;DataBase=generic_project_asp_net;Uid=root;Pwd=admin123";

builder.Services.AddDbContext<MySqlContext>(
    dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
    // The following three options help with debugging, but should
    // be changed or removed for production.
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    );

builder.Services.AddApiVersioning();

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
