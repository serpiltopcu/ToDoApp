using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;
using ToDoApp.Persistence.Providers;

namespace ToDoApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCouchbase(Configuration.GetSection("Couchbase"))
                .AddCouchbaseBucket<IUserBucketProvider>("Users");

            services.AddCouchbaseBucket<ITodoListBucketProvider>("TodoList");
            services.AddTransient(x =>
   {
       var userBucket = x.GetRequiredService<IUserBucketProvider>();
       return new BucketContext(userBucket.GetBucket());
   });
            services.AddTransient(x =>
            {
                var todoBucket = x.GetRequiredService<ITodoListBucketProvider>();
                return new BucketContext(todoBucket.GetBucket());
            });



            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/User";
        });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            ConfigureToDoAppServices(services);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
                
            });
        }
        private void ConfigureToDoAppServices(IServiceCollection services)
        {


            // Add Application Layer
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();

            // Add Web Layer
            // services.AddAutoMapper(typeof(Startup)); // Add AutoMapper

        }



    }
}
