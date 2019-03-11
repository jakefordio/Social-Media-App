using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialMediaApp.API.Data;

namespace SocialMediaApp.API
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
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(); // Cross Origin Resource Sharing, CORS  - Security measure that controls which clients(Domains) can access our api
            //Telling our app about our AuthRepository and IAuthRepository
            //3 options, AddSingleton(), AddTransient(), AddScoped()
            //AddScoped() creates a single instance for each HTTP Request, and uses it scoped to that user's HTTP session.
            services.AddScoped<IAuthRepository, AuthRepository>(); //Now available for injection in our controllers.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();

            // The below CORS configuration is weak, security wise, allowing any HTTP origin, method, or header;
            // but it will work for development instead of adding a secure CORS policy, which would take more time.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //Allows us to talk to our angular app (localhost:4200)
            app.UseMvc(); // Must initialize CORS or anything else before Mvc usually.
        }
    }
}
