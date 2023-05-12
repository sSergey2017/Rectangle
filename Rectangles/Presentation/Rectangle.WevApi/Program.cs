using Serilog.Events;
using Serilog;
using Rectangle.Application.Common.Mapping;
using System.Reflection;
using Rectangle.Application;
using Rectangle.Persistence;
using Rectangle.WebApi.HealthChecks;
using Rectangle.Application.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Rectangle.WebApi.Middleware;
using Rectangle.WebApi.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Rectangle.WebApi.JWT.Models;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Host.UseSerilog();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRectangleDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DbConnection"), tags: new[] { "database" })
    .AddCheck<RectangleServiceHealthCheck>(nameof(RectangleServiceHealthCheck));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

//JWT Authentication
builder.Services.AddJWT(configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<RectangleDbContext>();
DbInitializer.Initialize(db);

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Rectangle API");
});
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});


app.Run();
