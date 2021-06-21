using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChickenFarm.FrontEnd.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChickenFarm.FrontEnd.Blazor.Data;
using ChickenFarm.FrontEnd.Domain;
using ChickenFarm.FrontEnd.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ChickenFarm.FrontEnd.Blazor
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRazorPages()
                .AddDapr();
            services.AddServerSideBlazor();
            services.AddMediatR(cfg => cfg.AsScoped(), typeof(NewTasksQuery).GetTypeInfo().Assembly);

            services.AddSingleton<WeatherForecastService>()
                .AddSingleton<ITasksService, TasksService>()
                .AddSingleton<IMessageService, MessageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            var pathBase = _configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app
                // .UseSerilogRequestLogging()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
        }
    }
}