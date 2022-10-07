using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurfsUp.Areas.Identity.Data;
using SurfsUp.Data;
using SurfsUp.Database;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SurfsUpContextConnection") ?? throw new InvalidOperationException("Connection string 'SurfsUpContextConnection' not found.");


// Add services to the container.
builder.Services.AddDbContext<SurfsUpContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<SurfsUpUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<SurfsUpContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "SurfsUp API - V1",
        Version = "v1",
        Description = "API for our own Joe.",
        TermsOfService = new Uri("http://joe.mama/"),
        Contact = new OpenApiContact
        {
            Name = "Joe Mama",
            Email = "joe.mama@joe.mama"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        }
    }
);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
