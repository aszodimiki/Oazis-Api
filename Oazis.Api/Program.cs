

using Microsoft.Data.Sqlite;
using Oazis.Domain.Mappings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
.AddComposers()
    .Build();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddDbContext<OazisContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("OazisDatabase")));

builder.Services.AddCors(options =>
    options.AddPolicy(name: "AllowOazis",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    ));

WebApplication app = builder.Build();
var env = app.Services.GetRequiredService<IHostEnvironment>();
Console.WriteLine($"Environment!!!: {env.EnvironmentName}");

string connectionString = "Data Source=/var/www/data/OazisDB.db;Cache=Shared;Foreign Keys=True;Pooling=True";

using (var connection = new SqliteConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Database connection successful.");
    }
    catch (SqliteException ex)
    {
        Console.WriteLine($"SQLite error: {ex.Message}");
    }
}

await app.BootUmbracoAsync();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthorization();
app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        //u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
