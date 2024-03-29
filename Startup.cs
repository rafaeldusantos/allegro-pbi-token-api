﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allegro_pbi_token_api.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace allegro_pbi_token_api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value; 
                options.Database 
                    = Configuration.GetSection("MongoConnection:Database").Value; 

            });
            services.AddTransient<IOrganizationContext, OrganizationContext>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }
            app.UseCors("AllowAll");
            app.UseMvc();
            
        }
    }
}
