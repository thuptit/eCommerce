using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using eCommerce.Application.Hubs;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using eCommerce.Host;
using eCommerce.Shared.Cores.DependencyInjections;
using eCommerce.Shared.Cores.Responses;
using eCommerce.Shared.Extensions;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new WindsorServiceProviderFactory())
    .ConfigureContainer<WindsorContainer>(options =>
    {
        options.AddWindsorCastle();
    });

var configuration = LocalConfigurationExtentions.GetConfigurationBuilder();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithExceptionDetails()
    .WriteTo.Debug()
    .WriteTo.File("Logs/log.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
    
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var resourceBuilder = ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName);
builder.Services.AddOpenTelemetry()
    .WithTracing(options =>
    {
        options.AddSource("eCommerce.Application.Authentications", "eCommerce.Application.Users")
        .AddConsoleExporter();
    });
builder.Services.AddLogging(options => options.AddSerilog(Log.Logger));
builder.Services.AddSingleton(configuration);
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddApplication<eCommerceHostModule>();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapHub<ChattingHub>("/signalr-notification");
app.UseMiddleware<WrapperResponseMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UnitOfWorkMiddleware>();
app.MapControllers();
app.Run();