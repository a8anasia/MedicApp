using MedicApp.Configuration;
using MedicApp.Data;
using MedicApp.Repositories;
using MedicApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MedicApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<MedicAppDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddScoped<IPatientService, PatientService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //login authentication with cookies
            builder.Services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/User/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

