using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Customzito.Areas.Identity.Data;
using CustomBancoLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CZContext>(options => 
                options.UseSqlServer(connectionString));

builder.Services.AddScoped<UserManager<AspNetUsers>>();
//builder.Services.AddScoped<UserManager<CustomzitoUser>>();

builder.Services.AddDefaultIdentity<AspNetUsers>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CZContext>()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

builder.Services.AddMvc();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 90;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.Cookie.Name = "AuthCookie";
        config.LoginPath = "/Identity/Account/Login";
        config.AccessDeniedPath = "/Identity/Account/AccessDenied";
        config.LogoutPath= "/Identity/Account/Logout";
        config.ReturnUrlParameter = "returnUrl";
        config.SlidingExpiration = true;
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Adicione a autoriza��o aqui se necess�rio
        ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
        ctx.Context.Response.Headers.Add("Pragma", "no-cache");
        ctx.Context.Response.Headers.Add("Expires", "-1");
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
