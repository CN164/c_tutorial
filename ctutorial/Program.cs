using ctutorial.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ctutorial.BusinessFlow;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ProjectStartup();

void ProjectStartup()
{
    SetupDbContext();
    AddScopedBusinessFlow();
    AddScopedAndSingletonToService();
    builder.Services.AddCors();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHttpClient();

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }).AddRazorRuntimeCompilation();
    builder.Services.AddEndpointsApiExplorer();
    WebApplication app = builder.Build();
    app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
    app.UseRouting();
    app.MapControllers();
    app.Run();
}

void SetupDbContext()
{
    builder.Services.AddDbContext<MasterContext>(option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
    });
    builder.Services.AddDbContext<ReplicaContext>(option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("postgresReadOnly"));
    });
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

void AddScopedBusinessFlow()
{
    builder.Services.AddScoped<HealthCheckBusinessFlow>();
}

void AddScopedAndSingletonToService()
{
    builder.Services.AddScoped<MainContext>();
}
