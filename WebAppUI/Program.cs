using Autofac.Extensions.DependencyInjection;
using Autofac;
using DataAccess.Concrete.EntityFramework;
using Business.DependencyResolvers.Autofac;
using WebAppUI.Models;
using WebAppUI.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


// Add services to the container.
builder.Services.AddControllersWithViews();

// Model
builder.Services.AddSingleton<ProductListModel>();



// Identity & Database

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
             options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ECommerceDb;integrated security=true;"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();


// Password rules
builder.Services.Configure<IdentityOptions>(options =>
{
    // password

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //// options.User.AllowedUserNameCharacters = "";
    //options.User.RequireUniqueEmail = true;

    //options.SignIn.RequireConfirmedEmail = false;
    //options.SignIn.RequireConfirmedPhoneNumber = false;
});

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "KonusarakOgrenCase.Security.Cookie"
    };

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    SeedDatabase.Seed();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
     name: "adminProducts",
     pattern: "{controller=Home}/{action=ProductList}"
    );



//app.CustomStaticFiles();
app.Run();
