using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChickenFarm.FarmService.Infrastructure;
using ChickenFarm.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ChickenFarm.FarmService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<FarmContext>(options => options.UseNpgsql
                (
                    _configuration.GetConnectionString("RockstarMusicDb")
                ))
                .AddScoped<IRepository<Farm>, FarmRepository>()
                .AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ChickenFarm.FarmService.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FarmContext farmContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChickenFarm.FarmService.Api v1"));
            }

            farmContext.Database.EnsureDeleted();
            farmContext.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}