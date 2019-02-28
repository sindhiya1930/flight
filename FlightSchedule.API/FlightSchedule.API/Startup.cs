using FlightSchedule.Api.Filters;
using FlightSchedule.API.DataAccess;
using FlightSchedule.API.DB;
using FlightSchedule.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API
{
    [ExcludeFromCodeCoverage]
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
            services.AddSingleton(typeof(IDBAccess<FltSeg>), typeof(FlightSchedDataAccess));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

           
            services.AddMvc(opt =>
                            opt.Filters.Add(new ExceptionHandlingAttribute()));
            services.AddMvc(opt =>
                            opt.Filters.Add(new AuditFilterAttribute()));

            services.AddDbContext<AlaskaPoCDBContext>(sql =>
            sql.UseSqlServer("Server=alaskadb.database.windows.net;Database=AlaskaPoCDB;User Id=sysadmin;password=Password-1"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "V1",
                    Title = "FlightSchedule API",
                    Description = "FlightSchedule API",
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightSchedule API");
            });
        }
    }
}
