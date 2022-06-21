using Microsoft.EntityFrameworkCore;
using Multichoice_project.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

//tránh vòng lặp
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<Multichoise_DBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}/{id?}"
);

//---------
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});
//----------

app.Run();
