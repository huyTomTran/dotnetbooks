using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dotnetbooks.Models;

namespace dotnetbooks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
               opt.UseMySQL(Configuration.GetConnectionString("dotnetbooksDB")));


            services.AddCors();
            // services.AddCors(options => options.AddPolicy(name: MyAllowSpecificOrigins,
            //                                                 builder => builder
            //                                                             .AllowAnyMethod()
            //                                                             .AllowAnyHeader()
            //                                                             .AllowCredentials()
            //                                                             .WithOrigins(Configuration["Frontend"],
            //                                                                             "https://dotnetbooks.azurewebsites.net"
            //                                                             )));


            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // the CORS policy is needed when we are using 'localhost:3000' instead of 'nmpScript:start'
            // global CORS policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true) // allow any origin
                // .WithOrigins("http://localhost:3000")   // NOT WORKING ???
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()); // allow credentials
            // app.UseCors(MyAllowSpecificOrigins);


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
