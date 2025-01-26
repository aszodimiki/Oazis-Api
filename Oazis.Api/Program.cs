using Microsoft.AspNetCore.HttpOverrides;
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

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

await app.BootUmbracoAsync();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});


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

app.UseAuthorization();
app.UseCertificateForwarding();

await app.RunAsync();
