using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskApp.Areas.Identity.Data;
using TaskApp.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TaskAppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'TaskAppDbContextConnection' not found.");

builder.Services.AddDbContext<TaskAppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TaskAppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Eposta doğrulama gerektirilsin
    options.Password.RequireUppercase = false; // Büyük harf zorunluluğunu kaldır
})
    .AddRoles<IdentityRole>() // Roller için IdentityRole kullan
    .AddEntityFrameworkStores<TaskAppDbContext>();

// Email gönderme servisinin yapılandırılması
builder.Services.AddTransient<IEmailSender, EmailSenderImplementation>(); 

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Admin sayfasına yönlendirme eklenmesi
app.MapGet("/Admin", () =>
{
    var urlHelper = app.Services.GetRequiredService<IUrlHelper>();
    return Results.Redirect(urlHelper.Page("/Home/Admin"));
});

app.Run();
