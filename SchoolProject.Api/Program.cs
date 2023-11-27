using JWTApi.Data.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Infrustructure;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

#region DependenciesInjection

builder.Services
    .ServiceRegistration(builder.Configuration)
    .AddInfrustructureDependencies()
    .AddServicesDependencies()
    .AddCoreDependencies();

#endregion DependenciesInjection

#region Localization

builder.Services.AddLocalization(opt =>
 {
     opt.ResourcesPath = "";
 });

builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ar-EG")
        };

        options.DefaultRequestCulture = new RequestCulture("ar-EG");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

#endregion Localization

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region LOcalization MiddelWare

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

#endregion LOcalization MiddelWare

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();