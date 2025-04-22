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

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = ".MyApp.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



//DataAccess Layer and Interfaces services
builder.Services.AddDALServices();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSingleton<CaptchaService>();

//builder.Services.AddSingleton<HtmlHelperExtensions>();
builder.Services.AddControllersWithViews()
    .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);


//for session (Service)
builder.Services.AddSession();


builder.Services.AddDbContext<cvois_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=SuperAdmin}/{action=SuperAdminDashboard}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
