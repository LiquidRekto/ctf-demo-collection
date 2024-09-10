using demo_RaceCondition.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<demo_bypassContext>(option => option.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session tồn tại
    options.Cookie.HttpOnly = true; // Bảo mật cookie
    options.Cookie.IsEssential = true; // Cookie bắt buộc cho session
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/data-keys"))
    .SetApplicationName("demobypass");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
