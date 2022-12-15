using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ProyFinalV2.Models;
using Microsoft.AspNetCore.Identity;
using ProyFinalV2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var connectionString = builder.Configuration.GetConnectionString("Ecommerce2Context");
builder.Services.AddDbContext<Ecommerce2Context>(x => x.UseSqlServer(connectionString));

var connectionString2 = builder.Configuration.GetConnectionString("LoginContextConnection");
builder.Services.AddDbContext<LoginContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LoginContext>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
