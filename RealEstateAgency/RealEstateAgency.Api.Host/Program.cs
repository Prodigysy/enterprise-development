using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Application;
using RealEstateAgency.Application.Contracts;
using RealEstateAgency.Application.Contracts.Counterparty;
using RealEstateAgency.Application.Contracts.RealEstate;
using RealEstateAgency.Application.Contracts.RealEstateApplication;
using RealEstateAgency.Application.Service;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Data;
using RealEstateAgency.Domain.Model;
using RealEstateAgency.Infrastructure.EfCore;
using RealEstateAgency.Infrastructure.EfCore.Repository;
using RealEstateAgency.Infrastructure.RabbitMq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new RealEstateAgencyProfile());
});

builder.Services.AddSingleton<RealEstateSeeder>();

builder.Services.AddScoped<IRepository<Counterparty, int>, CounterpartyRepository>();
builder.Services.AddScoped<IRepository<RealEstate, int>, RealEstateRepository>();
builder.Services.AddScoped<IRepository<RealEstateApplication, int>, RealEstateApplicationRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int>, CounterpartyService>();
builder.Services.AddScoped<IApplicationService<RealEstateDto, RealEstateCreateUpdateDto, int>, RealEstateService>();
builder.Services.AddScoped<IRealEstateApplicationService, RealEstateApplicationService>();

builder.AddServiceDefaults();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("RealEstateAgency"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddNpgsqlDbContext<RealEstateAgencyDbContext>("DatabaseConnection");

builder.Services.AddHostedService<RealEstateAgencyRabbitMqConsumer>();

builder.AddRabbitMQClient("real-estate-agency-rabbitmq");

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<RealEstateAgencyDbContext>();

        await context.Database.MigrateAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
