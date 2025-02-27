using LSDCS.DataAccess.Context;
using LSDCS.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using LSDCS.Service.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NToastNotify;
using LSDCS.Service.Describers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

namespace LSDCS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Veritaban� ve di�er data layer yap�land�rmalar�  


            builder.Services.LoadDataLayerExtension(builder.Configuration);

            // Servis katman� yap�land�rmalar�
            builder.Services.LoadServiceLayerExtension();
            builder.Services.AddSession();
            // Identity yap�land�rmas�
            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            })
            .AddRoleManager<RoleManager<AppRole>>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<LSDCSDbContext>()
            .AddDefaultTokenProviders();

            // Cookie yap�land�rmas�
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = new PathString("/Admin/Auth/Login");
                config.LogoutPath = new PathString("/Admin/Auth/Logout");
                config.Cookie = new CookieBuilder
                {
                    Name = "LSDCS",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,    // Canl� ortam i�in Always olarak de�i�tirilmelidir.
                };

                config.SlidingExpiration = true;
                config.ExpireTimeSpan = TimeSpan.FromDays(1);
                config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
            });



            // MVC ve RazorPages ile dinamik i�erik olu�turma
            builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
            {
                PositionClass = ToastPositions.TopRight,
                TimeOut = 3000
            }).AddRazorRuntimeCompilation();



            var app = builder.Build();

            // Uygulama pipeline'�n� yap�land�rma
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseNToastNotify();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();



            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //           name: "default",
            //           pattern: "{area=Admin}/{controller=MailLog}/{action=Index}/{id?}");

            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                       pattern: "{area=Admin}/{controller=Auth}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.Run();
        }
    }
}
