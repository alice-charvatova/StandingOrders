using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StandingOrders.API.Filters;
using StandingOrders.API.Repositories;
using StandingOrders.API.Contexts;
using StandingOrders.API.Validators;
using StandingOrders.API.Entities;
using StandingOrders.API.Services;
using StandingOrders.API.Services.Encryption;
using StandingOrders.API.Models.Entities;

namespace StandingOrders.API

{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => 
                { 
                    options.EnableEndpointRouting = false;  
                    options.Filters.Add(typeof(ExceptionFilter)); 
                    options.Filters.Add(typeof(LogActionFilter));

                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StandingOrderValidator>());

            var connectionString = _configuration["connectionStrings:standingOrdersDBConnectionString"];
            services.AddDbContext<IB_SampleContext>(o => 
            {
                o.UseSqlServer(connectionString);

            });

            services.AddScoped<IRepository<StandingOrder>, Repository<StandingOrder>>();
            services.AddScoped<SecondFactorAuthorizationFilter>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddScoped<IRepository<Interval>, Repository<Interval>>();
            services.AddScoped<IRepository<ConstantSymbol>, Repository<ConstantSymbol>>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {             
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = "StandingOrder.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();

            loggerFactory.AddFile($"logs/log-{DateTime.Now.Ticks}.log");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Standing orders API");
            });
        }
    }
}

