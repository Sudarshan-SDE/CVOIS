using CVOIS.Models;
using CVOIS.Services;
using Microsoft.EntityFrameworkCore;
using CVOIS.DataAccessLayer.Admin;
using CVOIS.Interfaces.Admin;
using CVOIS.DataAccessLayer.Viewer;
using CVOIS.Interfaces.IViewer;
using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.DataAccessLayer.SuperAdmin_DAL;
using CVOIS.DataAccessLayer;
using CVOIS.Models.Connection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Options;

var time = 30;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddControllersWithViews();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(time);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy= CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

//DataAccess Layer and Interfaces services
builder.Services.AddDALServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<CaptchaService>();

//builder.Services.AddSingleton<HtmlHelperExtensions>();
builder.Services.AddControllersWithViews().AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.AddDbContext<cvois_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(time);
        options.Cookie.HttpOnly = true;
        options.LogoutPath = "/Home/Logout";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
pattern: "{controller=SuperAdmin}/{action=SuperAdminDashboard}/{id?}");
//pattern: "{controller=Home}/{action=Login}/{id?}");
//pattern: "{controller=Admin}/{action=AdminDashboard}/{id?}");

app.Run();
