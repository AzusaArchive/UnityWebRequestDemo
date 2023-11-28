using System.Reflection;
using Azusa.Shared.AspNetCore.Filters;
using Azusa.WeatherApi.WebApi.Primitives.Options;
using Azusa.WeatherApi.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var assembly = Assembly.GetExecutingAssembly();
    options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(assembly.Location),assembly.GetName().Name + ".xml"), 
        true);
});

builder.Services.Configure<MvcOptions>(options => options.Filters.Add(typeof(AzusaExceptionFilter)));
builder.Services.AddSingleton<CaiYunWeatherService>();
builder.Services.AddSingleton<MockWeatherService>();
builder.Services.AddHttpClient();

builder.Configuration.AddJsonFile("apiKeys.json", false,true);
builder.Services.Configure<ApiKeysOptions>(builder.Configuration.GetSection("ApiKeys"));

var app = builder.Build();

// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();