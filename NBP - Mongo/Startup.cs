using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NBP___Mongo", Version = "v1" });
            });

            services.AddSingleton<IDbClient, DbClient>();
            services.AddSingleton<CarService>();

            services.AddSingleton<UserService>();

            services.AddSingleton<DealerService>();

            services.AddSingleton<RentCarService>();

            services.AddSingleton<TestDriveService>();


            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins(new string[]
                    {
                        "http://localhost:8080",
                        "https://localhost:8080",
                        "http://127.0.0.1:8080",
                        "https://127.0.0.1:8080",
                        "http://localhost:5500",
                        "https://localhost:5500",
                        "http://127.0.0.1:5500",
                        "https://127.0.0.1:5500",
                        "https://localhost:5001",
                        "https://127.0.0.1:5001",
                        "http://127.0.0.1:5001",
                        "https://127.0.0.1:5001",
                        "http://localhost:3000",
                        "https://localhost:3000",
                        "https://localhost:5001",
                        "http://localhost:5001",






                        "http://localhost:3000",
                        "https://localhost:3000",
                        "http://127.0.0.1:3000"


                    })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();

                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NBP___Mongo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
        }
    }
}
