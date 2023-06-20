
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Receiving;
using Rentals;
using Purchasing;
using WebApp.Data;
using SecurityDependencies;
using Sales;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Added these lines below, need one connection string only, please use eToolsconnectionString, when you set up your user secret, use name "eToolsBD" for the string.
var eToolsconnectionString = builder.Configuration.GetConnectionString("eToolsDB");
//security sonnection
builder.Services.SecuritySystemBackendDependencies(options => options.UseSqlServer(eToolsconnectionString));
//------------
builder.Services.ReceivingBackendDependencies(options => options.UseSqlServer(eToolsconnectionString));
builder.Services.RentalsBackendDependencies(options => options.UseSqlServer(eToolsconnectionString));
builder.Services.SalesBackendDependencies(options => options.UseSqlServer(eToolsconnectionString));
builder.Services.PurchasingBackendDependencies(options => options.UseSqlServer(eToolsconnectionString));

//------------------------------------------------------------------------

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
