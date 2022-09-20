using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Areas.Identity.Data;
using SurfsUp.Data;
using SurfsUp.Database;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SurfsUpContextConnection") ?? throw new InvalidOperationException("Connection string 'SurfsUpContextConnection' not found.");

builder.Services.AddDbContext<SurfsUpContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SurfsUpUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<SurfsUpContext>();

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "1040558009655-5fsqbqtvhn8qi28h73cr58h5ii9bs5i0.apps.googleusercontent.com";
    options.ClientSecret = "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";
}); 
   



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.UseRequestLocalization(new RequestLocalizationOptions().SetDefaultCulture("de-DE"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
