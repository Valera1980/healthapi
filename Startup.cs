using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
           {
               options.AddPolicy(MyAllowSpecificOrigins,
               builder =>
                       {
                           builder.WithOrigins("http://localhost:3000", "http://192.168.1.194:3000", "'http://172.17.0.2")
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                       });
           });
            services.AddHttpClient();
            services.AddControllers();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration.GetValue<string>("IdentityServer:url");
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";
                });
            try
            {

                services.AddDbContext<AppMainContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("data base connection error");
                System.Console.WriteLine(ex.Message);
            }
            // DI
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IBodyDataRepository, BodyDataRepository>();
            services.AddScoped<IBodyDataTable, BodyDataTableService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // CORS
            app.UseCors(MyAllowSpecificOrigins);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
