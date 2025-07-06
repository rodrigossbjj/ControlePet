using ControlePetWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ControlePetWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona serviços MVC
            builder.Services.AddControllersWithViews();

            // Configura DbContext com SQL Server
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging() // <- log completo
                .LogTo(Console.WriteLine, LogLevel.Information));

            // Configura autenticação com cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";  // Página de login (GET)
                    options.LogoutPath = "/Login/Sair";  // Página para logout
                });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // IMPORTANTE: ordem correta dos middlewares
            app.UseAuthentication();
            app.UseAuthorization();

            // Rota padrão
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=PaginaInicial}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
