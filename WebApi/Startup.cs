using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();

            services.AddSwaggerGen(x => 
            {
                x.SwaggerDoc(Configuration.GetSection("Swagger:Version").Value, new OpenApiInfo()
                {
                    Title = Configuration.GetSection("Swagger:Title").Value,
                    Description = Configuration.GetSection("Swagger:Description").Value,
                    Contact = new OpenApiContact() { Name = Configuration.GetSection("Swagger:Contact:Name").Value, Email = Configuration.GetSection("Swagger:Contact:Email").Value },
                    License = new OpenApiLicense() { Name = Configuration.GetSection("Swagger:License:Name").Value, Url = new Uri(Configuration.GetSection("Swagger:License:Url").Value) }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(Configuration.GetSection("Swagger:Endpoint").Value, Configuration.GetSection("Swagger:Version").Value);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
