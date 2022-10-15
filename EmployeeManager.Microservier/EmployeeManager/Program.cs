using EmployeeManager;
using EmployeeManager.Domain.Core.Mapper.MappingProfiles;
using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Domain.Interfaces.DatabaseContext;
using EmployeeManager.Handlers;
using EmployeeManager.Infrastructure.Business.Services;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Services.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Employee Manager", 
        Version = "v1",
        Description = "Demo Api for employee managment"
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var configHelper = new ConfigHelper(builder.Configuration);
var connectionString = configHelper.GetDefaultConnectionString();
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DepartmentProfile>();
    cfg.AddProfile<EmployeeProfile>();
    cfg.AddProfile<RoleProfile>();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var supportedCultures = builder.Configuration.GetSection("Cultures").Get<string[]>();

var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .SetDefaultCulture("en-US");

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(localizationOptions);

app.UseMiddleware<ExceptionHandler>();

app.UseAuthorization();

app.MapControllers();



app.Run();
