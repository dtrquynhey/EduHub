using EduHubLibrary.DataAccess;
using EduHubLibrary.Services;
using EduHubWeb.Data;
using EduHubWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("EduHubConnectionString") ?? throw new InvalidOperationException("Connection string 'EduHubConnectionString' not found.");

var identityConnectionString = builder.Configuration.GetConnectionString("IdentityEduHubConnectionString") ?? throw new InvalidOperationException("Connection string 'IdentityEduHubConnectionString' not found.");

builder.Services.AddDbContext<EduHubDbContext>(options =>
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))) ;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddScoped<DataMappingService>();

builder.Services.AddScoped<IdentitySeederService>();

builder.Services.AddScoped<PopulateRolesFilter>();

builder.Services.AddScoped<CampaignServices>();

builder.Services.AddScoped<InteractionServices>();

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(PopulateRolesFilter));
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seederService = scope.ServiceProvider.GetRequiredService<IdentitySeederService>();
    await seederService.SeedIdentityRolesAsync();
    await seederService.SeedDefaultAdminAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
