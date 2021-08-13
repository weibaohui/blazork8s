using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using server.Middleware;
using server.Service.K8s;

namespace server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => { builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader(); });
            });

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "server", Version = "v1" }); });
            services.AddSingleton<Watcher>();
            services.AddSingleton<PodWatcher>();
            services.AddSingleton<NodeWatcher>();
            services.AddSingleton<RequestLoggingMiddleware>();

            #region HttpLoging

            // services.AddHttpLogging(logging =>
            // {
            //     // Customize HTTP logging here.
            //     logging.LoggingFields = HttpLoggingFields.All;
            //     logging.RequestHeaders.Add("My-Request-Header");
            //     logging.ResponseHeaders.Add("My-Response-Header");
            //     logging.MediaTypeOptions.AddText("application/javascript");
            //     logging.RequestBodyLogLimit  = 4096;
            //     logging.ResponseBodyLogLimit = 4096;
            // });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "server v1"));
            }

            app.UseHttpsRedirection();
            // app.UseHttpLogging();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
