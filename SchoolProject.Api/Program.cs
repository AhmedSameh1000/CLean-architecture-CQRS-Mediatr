using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using RefreshToken.Seeding;
using SchoolProject.Core;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder();

// Add services to the container.

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

using (var Scope = app.Services.CreateScope())
{
    var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await new SeedingData(RoleManger, UserManger).SeedData();
}

//if (!DbContext.Roles.Any())
//{
//    var RoleManger = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
//    var Usermanger = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
//    var SeedRoles = new SeedingData(RoleManger, Usermanger);
//    await SeedRoles.SeedData();
//}

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