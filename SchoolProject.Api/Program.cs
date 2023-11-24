using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DependenciesInjection

builder.Services
    .ServiceRegistration(builder.Configuration)
    .AddInfrustructureDependencies()
    .AddServicesDependencies()
    .AddCoreDependencies();

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false; // Set this to true to require at least one non-alphanumeric character
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

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