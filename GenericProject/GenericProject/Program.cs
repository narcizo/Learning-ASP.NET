using Microsoft.EntityFrameworkCore;
using Serilog;
using MySqlConnector;
using EvolveDb;

using GenericProject.Model;
using GenericProject.Business;
using GenericProject.Business.Implementations;
using GenericProject.Repository;
using GenericProject.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connect Database
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
var connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");

builder.Services.AddDbContext<MySqlContext>(
    dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
    // The following three options help with debugging, but should
    // be changed or removed for production.
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    );

// Migrations
if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connectionString);
}

// Add API Versioning
builder.Services.AddApiVersioning();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void MigrateDatabase(string connectionString)
{
    try
    {
        var evolveConnection = new MySqlConnection(connectionString);
        var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
        {
            // Directories where the migrations are located
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed. ", ex);
        throw;
    }
}