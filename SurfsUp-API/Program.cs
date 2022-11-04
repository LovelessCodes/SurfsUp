using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurfsUp_API.Database;
using SurfsUp_API.Areas.Identity.Data;
using SurfsUp_API.Models;
using Microsoft.OpenApi.Writers;

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

app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedRoles.Initialize(services);
    SeedSurfboards.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
    Log log = new Log
    {
        User = context.User.Identity.Name,
        Time = DateTime.Now,
        Message = "Accessed: " + context.Request.Path
    };
});

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
