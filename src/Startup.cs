using System.Runtime.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Prob
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly ApplicationVersionInfo _versionInfo;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _versionInfo = Assembly.GetExecutingAssembly().GetApplicationVersionInfo();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                // version 0.1.0 (= Current Semantic Version)
                c.SwaggerDoc($"v{_versionInfo.SemanticVersion}", new OpenApiInfo
                {
                    Title = $"{_versionInfo.Name} API - v{_versionInfo.SemanticVersion}",
                    Version = $"{_versionInfo.SemanticVersion}",
                    Description = "A test container for probing the runtime environment."
                });

                // version 0.2.0
                c.SwaggerDoc("v0.2.0", new OpenApiInfo
                {
                    Title = $"{_versionInfo.Name} API - v0.2.0",
                    Version = "0.2.0",
                    Description = "A test container for probing the runtime environment (v0.2.0)."
                });

                // provide swashbuckle with XML document comment.
                var xmlFile = $"{_versionInfo.Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v{_versionInfo.SemanticVersion}/swagger.json", $"Prob API - v{_versionInfo.SemanticVersion}");
                    c.SwaggerEndpoint($"/swagger/v0.2.0/swagger.json", "Prob API - v0.2.0");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
