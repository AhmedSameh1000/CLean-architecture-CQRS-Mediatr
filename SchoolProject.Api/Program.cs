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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/*

 System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[SchoolProject.Core.Feature.Authorization.Commands.Models.AddRoleCommand,SchoolProject.Core.Bases.Response`1[System.String]] Lifetime: Transient ImplementationType: SchoolProject.Core.Feature.Authorization.Commands.Handlers.RoleCommandHandler': Unable to resolve service for type 'Microsoft.Extensions.Localization.StringLocalizer`1[SchoolProject.Core.Resources.SharedResources]' while attempting to activate 'SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators'.) (Error while validating the service descriptor 'ServiceType: FluentValidation.IValidator`1[SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.IRolePropertiesValidation] Lifetime: Scoped ImplementationType: SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.RolePropertiesValidation': Unable to resolve service for type 'Microsoft.Extensions.Localization.StringLocalizer`1[SchoolProject.Core.Resources.SharedResources]' while attempting to activate 'SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.RolePropertiesValidation'.) (Error while validating the service descriptor 'ServiceType: SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.RolePropertiesValidation Lifetime: Scoped ImplementationType: SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.RolePropertiesValidation': Unable to resolve service for type 'Microsoft.Extensions.Localization.StringLocalizer`1[SchoolProject.Core.Resources.SharedResources]' while attempting to activate 'SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator.RolePropertiesValidation'.) (Error while validating the service descriptor 'ServiceType: FluentValidation.IValidator`1[SchoolProject.Core.Feature.Authorization.DTOs.AddRoleDto] Lifetime: Scoped ImplementationType: SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators': Unable to resolve service for type 'Microsoft.Extensions.Localization.StringLocalizer`1[SchoolProject.Core.Resources.SharedResources]' while attempting to activate 'SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators'.) (Error while validating the service descriptor 'ServiceType: SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators Lifetime: Scoped ImplementationType: SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators': Unable to resolve service for type 'Microsoft.Extensions.Localization.StringLocalizer`1[SchoolProject.Core.Resources.SharedResources]' while attempting to activate 'SchoolProject.Core.Feature.Authorization.Commands.Validator.RoleValidators'.)'

 */