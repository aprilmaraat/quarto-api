using System;
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
using Quarto.Api.Singleton;
using Quarto.Api.EF;
using Microsoft.EntityFrameworkCore;
using Quarto.Common.Package;

namespace Quarto.Api
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
            QuartoApi apiCache = Configuration.GetSection("Quarto").Get<QuartoApi>();
            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Business")));
            services.AddSingleton<IAppCache>(apiCache);
            services.AddControllers();
            services.AddCors();
        }

        private IConfigurationBuilder GetConfigurationBuilder(IWebHostEnvironment env)
        {
            return new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(t => t.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
