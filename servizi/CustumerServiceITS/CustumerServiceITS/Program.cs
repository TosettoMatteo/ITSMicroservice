using CustumerServiceITS.Datas;
using CustumerServiceITS.Repositories;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment}.json", optional: true)
               .Build();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                })
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddLogging(x => { x.ClearProviders(); x.AddSerilog(dispose: true); });

// Add services to the container.

builder.Services.AddScoped<ICustumerContext, CustumerContext>();
builder.Services.AddScoped<ICustumerRepository, CustumerRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseSerilogRequestLogging();

app.Run();
