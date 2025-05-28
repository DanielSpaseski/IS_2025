using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuseumApplication.Domain.IdentityModels;
using MuseumApplication.Repository.Data;
using MuseumApplication.Repository.Implementation;
using MuseumApplication.Repository.Interface;
using MuseumApplication.Service.Implementation;
using MuseumApplication.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<MuseumApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


// TODO: Configure the Dependency Injection for the services, adding them as Transient objects
// TODO: Configure the Dependency Injection for the Repository, adding it as Scoped object
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IArtifactService, ArtifactService>();
builder.Services.AddTransient<ICollectionService, CollectionService>();
builder.Services.AddTransient<IVisitorService, VisitorService>();
builder.Services.AddTransient<IVisitService, VisitService>();
builder.Services.AddTransient<IVisitorHistoryService, VisitorHistoryService>();


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
