using System.Reflection;
using ChickenFarm.TaskService.Application;
using ChickenFarm.TaskService.Domain;
using ChickenFarm.TaskService.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ChickenFarm.TaskService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<TaskDbContext>(options => options.UseNpgsql(_configuration.GetConnectionString("chickenfarm-db")))
                .AddScoped<IRepository<Farm>, FarmRepository>()
                .AddControllers()
                .AddDapr()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMediatR(cfg => cfg.AsScoped(), typeof(FarmsQuery).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ChickenFarm.TaskService.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TaskDbContext taskDbContext, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var pathBase = _configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            taskDbContext.Database.EnsureDeleted();
            taskDbContext.Database.EnsureCreated();
            taskDbContext.AddTestData();

            app
                // .UseSerilogRequestLogging()
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChickenFarm.TaskService.Api v1"))
                .UseRouting()
                .UseCloudEvents()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapSubscribeHandler();
                    endpoints.MapControllers();
                });
        }
    }
}