using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using P2_BDE_Events.Models.Compte;

namespace P2_BDE_Events
{
    public class Startup
    {
       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";
                    options.LogoutPath = "/Home/Index";
                    options.AccessDeniedPath = "/Home/Index";
                });

            services.AddControllersWithViews();
            services.AddMvc().AddRazorRuntimeCompilation();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireRoleAdmin", policy => policy.RequireRole("Administrateur"));
            //    options.AddPolicy("RequireRoleOrga", policy => policy.RequireRole("Organisateur"));
            //    options.AddPolicy("RequireRolePresta", policy => policy.RequireRole("Prestataire"));
            //    options.AddPolicy("RequireRoleParticip", policy => policy.RequireRole("Participant"));
            //});
            

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            using (BDDContext ctx = new BDDContext())
            {
                ctx.InitializeDb();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
