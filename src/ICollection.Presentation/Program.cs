using ICollection.Presentation.Configuration.LayerConfigurations;
using ICollection.Presentation.Midllewares;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureDataAccess(builder.Configuration);
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddService();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "ICollection.swagger", Version = "v2" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "ICollection.swagger");
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeakHub API V1");
    c.RoutePrefix = "area/swagger";
});
app.UseMiddleware<TokenRedirectMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        //context.HttpContext.Response.Redirect("login");
        context.HttpContext.Response.Redirect("accounts/login");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
   name: "administrators",
   areaName: "admins",
   pattern: "admins/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
   name: "administrators",
   areaName: "adminusers",
   pattern: "adminusers/{controller=Home}/{action=Index}/{id?}");

app.Run();
