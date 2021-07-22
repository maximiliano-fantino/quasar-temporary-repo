using ApiQF.Domain;
using ApiQF.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ApiQF
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
            services.AddSingleton(Configuration);

            var log = new Logger();
            services.AddSingleton(log);

            var topSecretSplit = new TopSecretSplit();
            services.AddSingleton(topSecretSplit);

            var satelliteConfig = new Config.SatelliteConfig();
            Configuration.Bind("SatellitesData", satelliteConfig);
            services.AddSingleton(satelliteConfig);

            services.AddSingleton(new TopSecretServices(log));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Top Secret API",
                    Description = "API para localizar y decodificar mensajes",
                    Contact = new OpenApiContact
                    {
                        Name = "Federico Marquestó",
                        Email = "fede.marquesto@gmail.com",
                        Url = new System.Uri("https://github.com/Fmarquesto/Quasar-Fire-Operation")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}