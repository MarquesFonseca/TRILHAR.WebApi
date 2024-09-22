//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TRILHAR.Services.Api.Extensions;
using System;
using System.IO;
using DinkToPdf;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.CrossCutting.IoC.Config;
using TRILHAR.Infra.Data;
using TRILHAR.Services.Api.Middlewares;
using TRILHAR.Infra.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Host.UseSerilog((context, services, config) => config
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var services = builder.Services;

services.Configure<Aplicacao>(configuration.GetSection("Aplicacao"));
var aplicacao = configuration.GetSection("Aplicacao").Get<Aplicacao>();

services.AddAutoMapperSetup();
services.AddControllers();
services.AddToken(configuration);
services.AddElmah();
services.AddSingleton(typeof(DinkToPdf.Contracts.IConverter), new SynchronizedConverter(new PdfTools()));
services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
services.AddDbContexts(configuration);
RegisterServices(services);
services.AddSwagger(configuration);
//services.AddCors(options =>
//{
//    options.AddPolicy("AllowMyOrigin", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader()
//               .AllowCredentials();
//    });
//});

var app = builder.Build();

app.UseHsts();
app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricherExtensions.EnrichFromRequest);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var urlV1 = $"{(!aplicacao.PathSwaggerFull ? "" : $"/TRILHAR")}/swagger/v1/swagger.json";
    c.SwaggerEndpoint(urlV1, "TRILHAR API V1");
});
app.UseAuthentication();
app.UseMiddleware<RequestSerilLogContextMiddleware>();
app.UseRouting();
app.UseCors("AllowMyOrigin");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseElmah();

app.Run();

void RegisterServices(IServiceCollection services)
{
    NativeInjectorBootStrapper.RegisterServices(services);
}