using ApiDevBP.ApplicationServices;
using ApiDevBP.Configuration;
using ApiDevBP.Infrastructure;
using ApiDevBP.Mappers;
using ApiDevBP.Repositories;
using ApiDevBP.Validations;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

#region Class Config
builder.Services.AddScoped<IUserValidator , UserValidator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserApplicationService>();
builder.Services.Configure<ConfigurationDB>(builder.Configuration.GetSection("DatabaseOptions"));

#endregion

#region Automapper Config
builder.Services.AddAutoMapper(typeof(MappingProfile));

try
{
    var mapperConfig = new MapperConfiguration(cfg => {
        cfg.AddProfile<MappingProfile>();
    });

    mapperConfig.AssertConfigurationIsValid();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Ocurrio un error Al configurar Automapper {DateTime.UtcNow}");
    throw ex;
}

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuration Serilog

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("serilog.json", optional: false, reloadOnChange: true) // Carga el archivo serilog.json
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
    });
    var xmlFilename = $"./Documentation.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


try
{
    Log.Information($"La Aplicación inició a las {DateTime.UtcNow}");
    #region app
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    #endregion
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Ocurrio un error {DateTime.UtcNow}");
}
finally
{
    Log.CloseAndFlush();
}


