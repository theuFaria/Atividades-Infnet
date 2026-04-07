using Microsoft.EntityFrameworkCore;
using RotaCerta.Data;
using RotaCerta.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core e string de conexão
builder.Services.AddDbContext<RotaCertaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RotaCertaContext>();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Roles.CriarRoles(services);

    var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var user = await _userManager.FindByEmailAsync("admin@gmail.com");

    if (user == null)
    {
        user = new ApplicationUser();
        user.UserName = "admin@gmail.com";
        user.Nome = "Administrador";
        await _userManager.CreateAsync(user, "Admin123*");
    }

    if (!await _userManager.IsInRoleAsync(user, "Admin"))
    {
        await _userManager.AddToRoleAsync(user, "Admin");
    }

    var _context = services.GetRequiredService<RotaCertaContext>();

    if (_context.Reservas.Where(r => !r.IsConfirmada).Any())
    {
        _context.Reservas.Where(r => !r.IsConfirmada).ExecuteDelete();
    }
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

app.Run();