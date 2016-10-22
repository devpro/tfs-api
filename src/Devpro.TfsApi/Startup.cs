using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Devpro.TfsApi
{
    public class Startup
    {
        private IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();

            services.AddSingleton<Lib.ITfsClientConfiguration>(new AppSettings
            {
                TfsCollectionUri       = GetConfigValue(_configuration, "TfsCollectionUri"),
                TfsPersonalAccessToken = GetConfigValue(_configuration, "TfsPersonalAccessToken")
            });
            services.AddScoped<Lib.ITfsClient, Lib.TfsClient>();
            services.AddTransient<Lib.IQueryService, Lib.QueryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // See. https://docs.asp.net/en/latest/fundamentals/logging.html
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Debug); // not intuitive, AddDebug is for the Debug Output window in Visual Studio with Information level per default

            app.UseMvc();
        }

        private static string GetConfigValue(IConfigurationRoot configuration, string key, string defaultValue = null)
        {
            var value = configuration.GetSection(key).Value;
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
