using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMangment.Models;
using EmployeeMangment.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeMangment
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {

                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;

            }).AddEntityFrameworkStores<AppDBContext>();



            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                                   policy => policy.AddRequirements(new ManageAdminRoleAndClaimRequirment()));

                options.AddPolicy("CreateRolePolicy",
                                   policy => policy.RequireClaim("Create Role"));
            });

            services.AddMvc(options => {

                var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddScoped<IEmployeeReposiotry, SQLEmployeeRepository>();
            
            services.AddSingleton<IAuthorizationHandler, canEditOnlyOtherAdminRoleAndClaimHandler>();

            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DeveloperExceptionPageOptions dop = new DeveloperExceptionPageOptions();
            dop.SourceCodeLineCount = 1;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(dop);
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();


            app.UseAuthentication();


            app.UseMvc(routes =>
            {

                routes.MapRoute("default", "{controller=Home}/{action=index}/{id?}");


            });
            // app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Some Error is getting");
            //    await context.Response.WriteAsync("Hello World");
            //});
        }
    }
}
