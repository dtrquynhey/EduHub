using EduHubLibrary.DataAccess;
using EduHubWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("EduHubConnectionString") ?? throw new InvalidOperationException("Connection string 'EduHubConnectionString' not found.");
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityEduHubConnectionString") ?? throw new InvalidOperationException("Connection string 'IdentityEduHubConnectionString' not found.");

builder.Services.AddDbContext<EduHubDbContext>(options =>
    options.UseSqlServer(connectionString));
    //.EnableSensitiveDataLogging()
    //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))) ;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserMappingService>();

var app = builder.Build();

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
