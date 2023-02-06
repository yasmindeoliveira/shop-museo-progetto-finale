global using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShopMuseoProgettoFinale.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

IServiceScope scope = app.Services
    .GetService<IServiceScopeFactory>()?
    .CreateScope();

if (scope is not null) {
    using (scope) {
        RoleManager<IdentityRole> roleManager = scope
            .ServiceProvider
            .GetService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Admin")) {
            _ = await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("Customer")) {
            _ = await roleManager.CreateAsync(new IdentityRole("Customer"));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    _ = app.UseExceptionHandler("/User/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run(url: "https://localhost:7128");
