using AngleSharp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using PortTime.Areas.Identity;
using PortTime.Data;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using System.Net;
using PortTime.BusinessLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7143/") });
builder.Services.AddSyncfusionBlazor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<CityControllerService>();
builder.Services.AddScoped<ICityController, CityControllerService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin());
});
builder.Services.AddControllersWithViews(opt => {
    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
}).AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
}).AddControllersAsServices();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin());
});
builder.Services.AddScoped<SfDialogService>();
builder.Services.AddHttpClient("weatherapi", c =>
{
    c.BaseAddress = new Uri("http://api.weatherapi.com");
    c.DefaultRequestHeaders.Add("User-Agent", "PortTime");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
    c.DefaultRequestHeaders.Add("key", "a1e2e92d456c4c12800171759230103");
});
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(2);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config => {
        config.ExpireTimeSpan = TimeSpan.FromHours(8);
        config.SlidingExpiration = true;
        config.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }
                return Task.FromResult(0);
            },
            OnSigningIn = ctx =>
            {
                return Task.FromResult(0);
            }
        };
    });


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

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(@"Mgo+DSMBaFt/QHRqVVhjVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSH5WdEViWXpfcndVRQ==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Td0VmW39fcHFURGRbUg==;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRjXH1ecXRQR2JaU0c=;ODg1NTA1QDMyMzAyZTM0MmUzMFkwcmcyQmxuUUt6NHJqVHIxei81NlJuQjgzZ2VmbFpyYkdsTVRQTnZCWlU9");


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
