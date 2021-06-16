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
using Microsoft.OpenApi.Models;

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
                .AddDbContext<TaskDbContext>(options =>
                    options.UseNpgsql(_configuration.GetConnectionString("FarmServiceDatabase")))
                .AddScoped<IRepository<Farm>, FarmRepository>()
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMediatR(cfg => cfg.AsScoped(), typeof(FarmsQuery).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ChickenFarm.TaskService.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TaskDbContext taskDbContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            taskDbContext.Database.EnsureDeleted();
            taskDbContext.Database.EnsureCreated();
            taskDbContext.AddTestData();

            app
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChickenFarm.TaskService.Api v1"))
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}